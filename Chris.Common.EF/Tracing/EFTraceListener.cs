namespace Chris.Common.EF.Tracing
{
    using System;

    using Clutch.Diagnostics.EntityFramework;

    public class EFTraceListener : IDbTracingListener
    {
        public void CommandExecuting(DbTracingContext context)
        {
            //implementation if needed here..
        }

        public void CommandFinished(DbTracingContext context)
        {
            //implementation if needed here..
        }

        public void ReaderFinished(DbTracingContext context)
        {
            //implementation if needed here..
        }

        public void CommandFailed(DbTracingContext context)
        {
            Console.WriteLine("\nFAILED\n " + context.Command.CommandText);
            // or Trace.WriteLine("\nFAILED\n " + context.Command.CommandText);
        }

        public void CommandExecuted(DbTracingContext context)
        {
            Console.WriteLine("\nExecuted\n " + context.Command.CommandText);
            // or Trace.WriteLine("\nExecuted\n " + context.Command.CommandText);
        }
    }
}