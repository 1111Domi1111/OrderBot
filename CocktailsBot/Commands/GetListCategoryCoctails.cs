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
    class GetListCategoryCoctails : Command
    {
        public override bool IsWorking { get; set; } = false;
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/getlistcategorycoctails";

        public static List<Command> commands { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            Bot = _bot;
            ApiClient api = new ApiClient();
            var result = await api.GetCategoryCoctails();
            SendInf(result, e);
            EndComand();
        }

        protected async void SendInf(ListCoctails results, Message message)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"Category coctails\n\n" +
               $"1.{results.Drinks[0].StrCategory} \n\n" +
               $"2.{results.Drinks[1].StrCategory}\n\n" +
               $"3.{results.Drinks[2].StrCategory}\n\n" +
               $"4.{results.Drinks[3].StrCategory} \n\n" +
               $"5.{results.Drinks[4].StrCategory}\n\n" +
               $"6.{results.Drinks[5].StrCategory}\n\n" +
               $"7.{results.Drinks[6].StrCategory} \n\n" +
               $"8.{results.Drinks[7].StrCategory}\n\n" +
               $"9.{results.Drinks[8].StrCategory}\n\n" +
               $"10.{results.Drinks[9].StrCategory} \n\n", parseMode: ParseMode.Html);
        }



        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
