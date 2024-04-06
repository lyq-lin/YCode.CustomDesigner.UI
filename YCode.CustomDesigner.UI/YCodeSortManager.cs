using System.Windows;
using System.Windows.Controls;

namespace YCode.CustomDesigner.UI
{
	internal class YCodeSortManager
	{
		private const double RepulsionForceFactor = 500.0;
		private const double SpringForceFactor = 0.05;
		private const double MaxVelocityMagnitude = 10.0; // 最大速度长度

		private Canvas _canvas;
		private List<YCodeNode>? _nodes;
		private List<Edge>? _edges;

		public YCodeSortManager(YCodeCanvas canvas)
		{
			_canvas = canvas;
		}

		public void Layout(int iterations)
		{
			_nodes = _canvas.Children.OfType<YCodeNode>().ToList();

			_edges = _canvas.Children
				.OfType<YCodeLine>()
				.Select(x => new Edge
				{
					SourceNodeId = x.Source.Name,
					TargetNodeId = x.Target.Name,
				})
				.ToList();

			// 迭代更新节点位置
			for (int i = 0; i < iterations; i++)
			{
				CalculateForces();
				UpdatePositions();
			}
		}

		private void CalculateForces()
		{
			// 计算斥力
			foreach (var node in _nodes)
			{
				var velocity = new Vector();

				foreach (var otherNode in _nodes)
				{
					if (node == otherNode)
						continue;

					Vector direction = otherNode.Position - node.Position;
					double distanceSquared = direction.LengthSquared;

					if (distanceSquared > 0)
					{
						double distance = Math.Sqrt(distanceSquared);
						double repulsionForce = RepulsionForceFactor / distanceSquared;

						velocity += repulsionForce * direction / distance;
					}
				}

				node.Velocity = velocity;
			}

			// 计算引力
			foreach (var edge in _edges)
			{
				var sourceNode = _nodes.Find(n => n.Name == edge.SourceNodeId);
				var targetNode = _nodes.Find(n => n.Name == edge.TargetNodeId);

				if (sourceNode != null && targetNode != null)
				{
					Vector direction = targetNode.Position - sourceNode.Position;
					double distanceSquared = direction.LengthSquared;

					if (distanceSquared > 0)
					{
						double distance = Math.Sqrt(distanceSquared);
						double springForce = SpringForceFactor * distance;

						sourceNode.Velocity += springForce * direction;
						targetNode.Velocity -= springForce * direction;
					}
				}
			}
		}

		private void UpdatePositions()
		{
			foreach (var node in _nodes)
			{
				// 根据速度更新节点位置
				Vector displacement = node.Velocity;

				if (displacement.Length > MaxVelocityMagnitude)
				{
					displacement.Normalize();
					displacement *= MaxVelocityMagnitude;
				}

				Point newPosition = node.Position + displacement;
				Canvas.SetLeft(node, newPosition.X);
				Canvas.SetTop(node, newPosition.Y);
			}
		}
	}

	internal class Edge
	{
		public string SourceNodeId { get; set; }
		public string TargetNodeId { get; set; }
	}
}
