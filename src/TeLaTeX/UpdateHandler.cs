using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeLaTeX;

public static class UpdateHandler
{
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        Console.WriteLine($"Received a '{messageText}' message in chat {message.Chat.Id}.");

        try
        {
            await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                replyToMessageId: update.Message.MessageId,
                photo: new InputFileUrl(UrlFactory.Create(messageText)), cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine($"failed with error : {e.Message}");
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Failed",
                replyToMessageId: update.Message.MessageId, cancellationToken: cancellationToken);
        }
    }
}