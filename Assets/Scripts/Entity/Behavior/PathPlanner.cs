using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Entity.Behavior {
    class ExplorationNode: IComparable<ExplorationNode> {
        public Vector2Int node;
        public Vector2Int target;
        public float distance;
        public Vector2Int? lastNode;
        
        public ExplorationNode(
            Vector2Int node,
            Vector2Int target,
            float distance = Single.PositiveInfinity,
            Vector2Int? lastNode = null
        ) {
            this.node = node;
            this.target = target;
            this.distance = distance;
            this.lastNode = lastNode;
        }

        public float heuristic => Mathf.Abs((target - node).x) + Mathf.Abs((target - node).y);
        public float estimatedDistance => heuristic + distance;

        public int CompareTo(ExplorationNode other) {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            
            return estimatedDistance.CompareTo(other.estimatedDistance);
        }
    }
    
    public class PathPlanner {
        public int intelligence = 128;
        public int realTimeIntelligence = 16;
        
        public EntityBase entity;
        public List<Vector2Int> plan = new List<Vector2Int>();
        public Vector2Int? target = null;

        public PathPlanner(EntityBase entity) {
            this.entity = entity;
        }

        public Vector2Int? GetNextLocation() {
            if (!target.HasValue)
                return null;
            
            if (this.plan.Count == 0) {
                CalculatePath(true);
            }
            
            if (this.plan.Count > 0) {
                var lastItem = this.plan.Count - 1;
                var item = this.plan[lastItem];
                this.plan.Remove(item);

                return item;
            } else {
                // Path not found
                return target;
            }
        }

        private Vector2Int? previousLocation;
        public void Update() {
            if (!previousLocation.HasValue) {
                previousLocation = GetNextLocation();
            }

            if (previousLocation.HasValue) {
                var finished = entity.MoveTowards(previousLocation.Value);
                if (finished) {
                    previousLocation = GetNextLocation();
                }
            }
        }

        public void MoveTo(Vector2Int block) {
            this.target = block;
            this.previousLocation = null;
            this.CalculatePath();
        }

        public void CalculatePath(bool isRealtime = false) {
            if (!this.target.HasValue)
                return;
            
            var queue = new SortedSet<ExplorationNode>();
            var nodes = new Dictionary<Vector2Int, ExplorationNode>();
            
            var maxExpansion = isRealtime ?
                this.realTimeIntelligence :
                this.intelligence;

            var start = new Vector2Int(entity.x, entity.y);
            var startNode = new ExplorationNode(
                start,
                this.target.Value,
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
                
                if (traversal.node == target) {
                    break;
                }

                if (!entity.world.IsAir(traversal.node.x, traversal.node.y)) {
                    continue;
                }
                
                var availNodes = new Vector2Int[] {
                    new Vector2Int(traversal.node.x + 1, traversal.node.y),
                    new Vector2Int(traversal.node.x - 1, traversal.node.y),
                    new Vector2Int(traversal.node.x, traversal.node.y + 1),
                    new Vector2Int(traversal.node.x, traversal.node.y - 1)
                };

                foreach (var nextNode in availNodes) {
                    if (!entity.world.IsAir(nextNode.x, nextNode.y)) {
                        Debug.Log("Skip " + nextNode);
                        continue;
                    }
                    
                    if (!nodes.ContainsKey(nextNode)) {
                        var nextNodeObject = new ExplorationNode(
                            nextNode,
                            this.target.Value,
                            traversal.distance + 1,
                            traversal.node
                        );
                        nodes.Add(nextNode, nextNodeObject);
                        queue.Add(nextNodeObject);
                    } else {
                        var nextNodeObject = nodes[nextNode];
                        if (nextNodeObject.distance > traversal.distance + 1) {
                            queue.Remove(nextNodeObject);
                            nextNodeObject.distance = traversal.distance + 1;
                            queue.Add(nextNodeObject);
                        }
                    }
                }

                Debug.Log("Expand " + traversal.node);
                expansion++;
            }

            plan.Clear();
            
            var planningNode = lastVisit;
            while (planningNode == startNode) {
                plan.Add(planningNode.node);

                if (!planningNode.lastNode.HasValue)
                    break;
                
                var previousNode = nodes[planningNode.lastNode.Value];
                planningNode = previousNode;
            }
            plan.Reverse();
        }
    }
}