using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using CocktailsBot.Clients;
using WebApplication1;
using CocktailsBot.Commands;

class GetOrderStatus : Command
{
    public override bool IsWorking { get; set; } = false;
    public override TelegramBotClient Bot { get; set; }
    public override string Name { get; set; } = "/getorderstatus";

    public static List<Command> commands { get; set; }

    public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
    {
        commands = _commands;
        Bot = _bot;
        try
        {
            ApiClient api = new ApiClient();
            var result = await api.GetOrderStatus($"{e.Chat.Id}");
            SendInf(result, e);
            EndComand();
        }
        catch
        {
            await Bot.SendTextMessageAsync(e.From.Id, $"You order - null\n\n");

        }

    }

    protected async void SendInf(OrderStatus results, Message message)
    {
        await Bot.SendTextMessageAsync(message.From.Id, $"You order status:\n\n");
             
            await Bot.SendTextMessageAsync(message.From.Id, $"{results.S} \n\n", parseMode: ParseMode.Html);

        

    }



    public override void EndComand()
    {
        IsWorking = false;
    }
}