using CocktailsBot.Clients;
using WebApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace CocktailsBot.Commands
{
    class Discount : Command
    {
    public override bool IsWorking { get; set; } = false;
    public override TelegramBotClient Bot { get; set; }
    public override string Name { get; set; } = "/discount";

    public static List<Command> commands { get; set; }

    public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
    {
        commands = _commands;
            Bot = _bot;
            ApiClient api = new ApiClient();
            var  result = await api.GetListDiscount();
             SendInf(result, e);
            EndComand();
    }
        protected async void SendInf(List<DBRepositoryDiscount> results, Message message)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"\n\n" +
               $"1.{results[0].Id} {results[0].Name} {results[0].Price}\n\n" +
               $"2.{results[1].Id} {results[1].Name} {results[1].Price}\n\n" +
               $"3.{results[2].Id} {results[2].Name} {results[2].Price}\n\n", parseMode: ParseMode.Html);
        }
        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
