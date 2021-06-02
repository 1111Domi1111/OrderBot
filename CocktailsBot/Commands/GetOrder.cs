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

class GetOrder : Command
{
    public override bool IsWorking { get; set; } = false;
    public override TelegramBotClient Bot { get; set; }
    public override string Name { get; set; } = "/getorder";

    public static List<Command> commands { get; set; }

    public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
    {
        commands = _commands;
        Bot = _bot;
        try
        {
            ApiClient api = new ApiClient();
            var result = await api.GetOrder($"{e.Chat.Id}");
         SendInf(result, e);
        EndComand();
        }
        catch
        {
            await Bot.SendTextMessageAsync(e.From.Id, $"You order - null\n\n");

        }
       
    }

    protected async void SendInf(OrderDBRepository results, Message message)
    {
        await Bot.SendTextMessageAsync(message.From.Id, $"You order\n\n");
        int a = 1;
        foreach (var item in results.Item.DishesOrCoctails.SS)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"{a}.{item} \n\n", parseMode: ParseMode.Html);
            a++;
        }
          
    }



    public override void EndComand()
    {
        IsWorking = false;
    }
}

