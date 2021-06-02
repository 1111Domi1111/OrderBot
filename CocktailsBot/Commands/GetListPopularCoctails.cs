
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
    class GetListPopularCoctails : Command
    {
        public override bool IsWorking { get; set; } = false;
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/getlistpopularcoctails";

        public static List<Command> commands { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            Bot = _bot;
            ApiClient api = new ApiClient();
            var result = await api.GetListPopularCoctails();
            SendInf(result, e);
            EndComand();
        }

        protected async void SendInf(ListCoctails results, Message message)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"Popular coctails\n\n" +
               $"1.{results.Drinks[0].IdDrink} {results.Drinks[0].StrDrink} \n\n" +
               $"2.{results.Drinks[1].IdDrink} {results.Drinks[1].StrDrink}\n\n" +
               $"3.{results.Drinks[2].IdDrink} {results.Drinks[2].StrDrink}\n\n" +
               $"4.{results.Drinks[3].IdDrink} {results.Drinks[3].StrDrink} \n\n" +
               $"5.{results.Drinks[4].IdDrink} {results.Drinks[4].StrDrink}\n\n" +
               $"6.{results.Drinks[5].IdDrink} {results.Drinks[5].StrDrink}\n\n" +
               $"7.{results.Drinks[6].IdDrink} {results.Drinks[6].StrDrink} \n\n" +
               $"8.{results.Drinks[7].IdDrink} {results.Drinks[7].StrDrink}\n\n" +
               $"9.{results.Drinks[8].IdDrink} {results.Drinks[8].StrDrink}\n\n" +
               $"10.{results.Drinks[9].IdDrink} {results.Drinks[9].StrDrink} \n\n", parseMode: ParseMode.Html);
        }



        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}