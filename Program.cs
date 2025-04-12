using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WFIN;

class Program {
    public static async Task Main() {
        var cts = new CancellationTokenSource();
        var bot = new TelegramBotClient("7376140194:AAHwtdYfVzLOHatuKsmRSOK4_HJkZEPIv_A", cancellationToken: cts.Token);
        var me = await bot.GetMe();

        Handlers handlers = new Handlers(bot);

        bot.OnMessage += handlers.onMessage;
        bot.OnUpdate += handlers.onUpdate;
        bot.OnError += handlers.onError;

        Console.WriteLine($"Bot started as {me.FirstName} ({me.Id}). Press Enter to stop the bot");  
        Console.ReadLine();
        cts.Cancel();
    }
}