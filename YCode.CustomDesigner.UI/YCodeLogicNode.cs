using System.Diagnostics;

namespace YCode.CustomDesigner.UI
{
	public class YCodeLogicNode : YCodeNode
	{
		public virtual void Process()
		{
			Debug.WriteLine($"I'm {this.Name}, Processed! ");

			foreach (var line in this.Lines)
			{
				if (line.Source == this && line.Target is YCodeLogicNode target)
				{
					target.Process();
				}
			}
		}
	}
}
