using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Behavior {
    class ExplorationNode: IComparable<ExplorationNode> {
        public Vector2Int Node;
        public Vector2Int Target;
        public float Distance;
        public Vector2Int? LastNode;
        
        public ExplorationNode(
            Vector2Int node,
            Vector2Int target,
            float distance = Single.PositiveInfinity,
            Vector2Int? lastNode = null
        ) {
            this.Node = node;
            this.Target = target;
            this.Distance = distance;
            this.LastNode = lastNode;
        }

        public float Heuristic => Mathf.Abs((Target - Node).x) + Mathf.Abs((Target - Node).y);
        public float EstimatedDistance => Heuristic + Distance;

        public int CompareTo(ExplorationNode other) {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            
            return EstimatedDistance.CompareTo(other.EstimatedDistance);
        }
    }
    
    public class PathPlanner {
        public int Intelligence = 128;
        public int RealTimeIntelligence = 16;
        
        public EntityBase Entity;
        public List<Vector2Int> Plan = new List<Vector2Int>();
        public Vector2Int? Target;

        public PathPlanner(EntityBase entity) {
            this.Entity = entity;
        }

        public Vector2Int? GetNextLocation() {
            if (!Target.HasValue)
                return null;
            
            if (this.Plan.Count == 0) {
                CalculatePath(true);
            }
            
            if (this.Plan.Count > 0) {
                var lastItem = this.Plan.Count - 1;
                var item = this.Plan[lastItem];
                this.Plan.Remove(item);

                return item;
            } else {
                // Path not found
                return Target;
            }
        }

        private Vector2Int? _previousLocation;
        public void Update() {
            if (!_previousLocation.HasValue) {
                _previousLocation = GetNextLocation();
            }

            if (_previousLocation.HasValue) {
                var finished = Entity.MoveTowards(_previousLocation.Value);
                if (finished) {
                    _previousLocation = GetNextLocation();
                }
            }
        }

        public void MoveTo(Vector2Int block) {
            this.Target = block;
            this._previousLocation = null;
            this.CalculatePath();
        }

        public void CalculatePath(bool isRealtime = false) {
            if (!this.Target.HasValue)
                return;
            
            var queue = new SortedSet<ExplorationNode>();
            var nodes = new Dictionary<Vector2Int, ExplorationNode>();
            
            var maxExpansion = isRealtime ?
                this.RealTimeIntelligence :
                this.Intelligence;

            var start = new Vector2Int(Entity.x, Entity.y);
            var startNode = new ExplorationNode(
                start,
                this.Target.Value,
                0
            );
            
            nodes.Add(start, startNode);
            queue.Add(startNode);
            
            var expansion = 0;
            var lastVisit = startNode;
            while (expansion <= maxExpansion && queue.Count > 0) {
                var traversal = queue.Min;
                queue.Remove(traversal);

                lastVisit = traversal;
                
                if (traversal.Node == Target) {
                    break;
                }

                if (!Entity.World.IsAir(traversal.Node.x, traversal.Node.y)) {
                    continue;
                }
                
                var availNodes = new[] {
                    new Vector2Int(traversal.Node.x + 1, traversal.Node.y),
                    new Vector2Int(traversal.Node.x - 1, traversal.Node.y),
                    new Vector2Int(traversal.Node.x, traversal.Node.y + 1),
                    new Vector2Int(traversal.Node.x, traversal.Node.y - 1)
                };

                foreach (var nextNode in availNodes) {
                    if (!Entity.World.IsAir(nextNode.x, nextNode.y)) {
                        Debug.Log("Skip " + nextNode);
                        continue;
                    }
                    
                    if (!nodes.ContainsKey(nextNode)) {
                        var nextNodeObject = new ExplorationNode(
                            nextNode,
                            this.Target.Value,
                            traversal.Distance + 1,
                            traversal.Node
                        );
                        nodes.Add(nextNode, nextNodeObject);
                        queue.Add(nextNodeObject);
                    } else {
                        var nextNodeObject = nodes[nextNode];
                        if (nextNodeObject.Distance > traversal.Distance + 1) {
                            queue.Remove(nextNodeObject);
                            nextNodeObject.Distance = traversal.Distance + 1;
                            queue.Add(nextNodeObject);
                        }
                    }
                }

                Debug.Log("Expand " + traversal.Node);
                expansion++;
            }

            Plan.Clear();
            
            var planningNode = lastVisit;
            while (planningNode == startNode) {
                Plan.Add(planningNode.Node);

                if (!planningNode.LastNode.HasValue)
                    break;
                
                var previousNode = nodes[planningNode.LastNode.Value];
                planningNode = previousNode;
            }
            Plan.Reverse();
        }
    }
}