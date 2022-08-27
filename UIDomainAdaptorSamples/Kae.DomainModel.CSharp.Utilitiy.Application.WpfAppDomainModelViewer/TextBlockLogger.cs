using Kae.Utility.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kae.DomainModel.CSharp.Utilitiy.Application.WpfAppDomainModelViewer
{
    public class TextBlockLogger : Logger
    {
        TextBlock tb;
        public TextBlockLogger(TextBlock tb)
        {
            this.tb = tb;
        }
        protected override Task LogInternal(Level level, string log, string timestamp)
        {
            return Task.Run(() =>
            {
                tb.Dispatcher.InvokeAsync(() =>
                {
                    var sb = new StringBuilder(tb.Text);
                    var writer = new StringWriter(sb);
                    writer.WriteLineAsync($"[{level.ToString()}]{timestamp}:{log}");
                });
            });
        }
    }
}