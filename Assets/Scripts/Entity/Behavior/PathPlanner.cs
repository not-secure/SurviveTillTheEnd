using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Entity.Behavior {
    public class PathPlanner {
        public int intelligence = 512;
        public int realTimeIntelligence = 16;
        
        public EntityBase entity;
        public List<Vector2Int> plan;
        public Vector2Int? target = null;

        public PathPlanner(EntityBase entity) {
            this.entity = entity;
        }

        public Vector2 GetNextLocation() {
            if (this.plan.Count > 0) {
                
            }

            // TODO implement
            throw new System.NotImplementedException();
        }

        public void Update() {
            
        }

        public void CalculatePath(bool isRealtime = false) {
            var queue = new PriorityQueue<int, Vector2Int>();
            var maxExpansion = isRealtime ?
                this.realTimeIntelligence :
                this.intelligence;

            var expansion = 0;
            while (expansion > maxExpansion || queue.Count() > 0) {
                var nextTraversal = queue.Dequeue();
                expansion++;
            }
        }
    }
}