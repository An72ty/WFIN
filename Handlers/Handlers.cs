using GTranslatorAPI;
using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using  System.Net.Http;
using System.Text.Json;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Data;

namespace WFIN;

public class Handlers {
    TelegramBotClient bot;

    public Handlers(TelegramBotClient client) {
        bot = client;
    }

    async public Task onMessage(Message msg, UpdateType type) {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://ftapi.pythonanywhere.com/");
        client.DefaultRequestHeaders.Referrer = new Uri("https://ftapi.pythonanywhere.com/");


        if (msg.Text is null) return;
        if (msg.Text == "/start") {
            await bot.SendMessage(msg.Chat, $"Привет, {msg.Chat.Username}! Этот бот позволяет переводить сообщения, напиши его и бот переведет его на английский и испанский", replyMarkup: new InlineKeyboardButton[]{"Author"});
        } else {
            HttpResponseMessage response = await client.GetAsync($"/translate?dl=en&text={msg.Text}");
            var json_str = await response.Content.ReadAsStringAsync();

            string en_msg = json_str.Split("\", \"")[3].Split(": ")[1].Trim('"');

            response = await client.GetAsync($"/translate?dl=es&text={msg.Text}");
            json_str = await response.Content.ReadAsStringAsync();

            string es_msg = json_str.Split("\", \"")[3].Split(": ")[1].Trim('"');

             await bot.SendMessage(msg.Chat, $"Ваше сообщение переведено\nАнглийский - {en_msg}\nИспанский - {es_msg}");
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