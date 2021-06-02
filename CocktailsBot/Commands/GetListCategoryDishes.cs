using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using CocktailsBot.Clients;
using WebApplication1;

namespace CocktailsBot.Commands
{
    class GetListCategoryDishes : Command
    {
        public override bool IsWorking { get; set; } = false;
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/getlistcategorydishes";

        public static List<Command> commands { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            Bot = _bot;
            ApiClient api = new ApiClient();
            var result = await api.GetListCategoryDishes();
            SendInf(result, e);
            EndComand();
        }

        protected async void SendInf(ListDish results, Message message)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"Category dishes\n\n" +
               $"1.{results.Results[0].Name} \n\n" +
               $"2.{results.Results[1].Name}\n\n" +
               $"3.{results.Results[2].Name}\n\n" +
               $"4.{results.Results[3].Name} \n\n" +
               $"5.{results.Results[4].Name}\n\n" +
               $"6.{results.Results[5].Name}\n\n" +
               $"7.{results.Results[6].Name} \n\n" +
               $"8.{results.Results[7].Name}\n\n" +
               $"9.{results.Results[8].Name}\n\n" +
               $"10.{results.Results[9].Name} \n\n", parseMode: ParseMode.Html);
        }



        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}