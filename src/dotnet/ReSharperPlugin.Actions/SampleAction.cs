using System;
using System.Net.Sockets;
using System.Text;
using JetBrains.Application.DataContext;
using JetBrains.Application.Threading;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Features.Internal.Resources;
using JetBrains.Rider.Model.UIAutomation;
using JetBrains.Util;

namespace ReSharperPlugin.Actions;

// Action ID = ClassName.TrimEnd("Action")
// This is also shown in Visual Studio Options > Keyboard > Shortcuts
[Action(
    ResourceType: typeof(Resources),
    TextResourceName: nameof(Resources.SlotSpin),
    DescriptionResourceName = nameof(Resources.SlotSpin),
    // Icon must also be changed in frontend code
    Icon = typeof(FeaturesInternalThemedIcons.QuickStartToolWindow))]
public class SampleAction : IExecutableAction
{
    public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
    {
        return true;
    }

    public void Execute(IDataContext context, DelegateExecute nextExecute)
    {
        CallUnitySpinFunction();
        Console.WriteLine("run");
    }
    
    private void CallUnitySpinFunction()
    {
        try
        {
            using (var client = new TcpClient("localhost", 12345))
            using (var stream = client.GetStream())
            {
                byte[] data = Encoding.UTF8.GetBytes("Spin");
                stream.Write(data, 0, data.Length);
            }
        }
        catch (SocketException ex)
        {
            // Handle the exception
        }
    }
}
