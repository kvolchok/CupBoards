using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Models;

namespace Services
{
    public class ChipMovingService
    {
        public async UniTask MoveChip(ChipModel currentChip, Stack<NodeModel> route)
        {
            var startNode = route.Pop();
            var previousNode = startNode;
            
            while (route.Count > 0)
            {
                var currentNode = route.Pop();
                var nextPosition = currentNode.Position;
                
                currentNode.SetChip(currentChip);
                previousNode.SetChip(null);

                previousNode = currentNode;
                
                await currentChip.ChangePosition(nextPosition);
            }
        }
    }
}