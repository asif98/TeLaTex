using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using System.IO;

namespace TeLaTeX
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var accessToken = await File.ReadAllTextAsync(@"D:\Coding\TeLaTeX\accessToken.txt");
            var botClient = new TelegramBotClient(accessToken)
            {
                Timeout = new TimeSpan(0,1,0),
            };
            
            using var cts = new CancellationTokenSource();

            botClient.StartReceiving(
                updateHandler: UpdateHandler.HandleUpdateAsync,
                pollingErrorHandler: ErrorHandler.HandlePollingErrorAsync,
                receiverOptions: new ReceiverOptions()
                {
                    // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
                    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
                },
                cancellationToken: cts.Token
            );
            
            var me = await botClient.GetMeAsync(cts.Token);
        
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();
        }
    }
}