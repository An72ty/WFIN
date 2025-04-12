using GTranslatorAPI;
using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WFIN;

class Handlers {
    TelegramBotClient bot;

    public Handlers(TelegramBotClient client) {
        bot = client;
    }

    async public Task onMessage(Message msg, UpdateType type) {
        if (msg.Text is null) return;
        if (msg.Text == "/start") {
            await bot.SendMessage(msg.Chat, $"Привет, {msg.Chat.Username}! Этот бот позволяет переводить сообщения, напиши его и бот переведет его на английский и испанский", replyMarkup: new InlineKeyboardButton[]{"Author"});
        }
    }


    async public Task onUpdate(Update update) {
        if (update is {CallbackQuery: {} query}) {
            await bot.AnswerCallbackQuery(query.Id, "@an72ty");
            await bot.SendMessage(query.Message!.Chat, "@an72ty");
        }
    }

    async public Task onError(Exception exception, HandleErrorSource source) {
        Console.WriteLine(exception);
    }
}