using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DotNetEnv;

namespace WFIN;

class Program {
    public static async Task Main() {
        Env.Load("token.env");
        string token = Environment.GetEnvironmentVariable("BOT_TOKEN")!;
        var cts = new CancellationTokenSource();
        var bot = new TelegramBotClient(token, cancellationToken: cts.Token);
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