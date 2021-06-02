using CocktailsBot.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebApplication1;

namespace CocktailsBot.Commands
{
    class GetListDishesByCategory : Command
    {

        public override bool IsWorking { get; set; } = false;
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/getlistdishesbycategory";

        public static List<Command> commands { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            Bot = _bot;
            await Bot.SendTextMessageAsync(e.From.Id, "Enter a category for the dish");
            Bot.OnMessage += GetString;
        }
        private async void GetString(object sender, MessageEventArgs e)
        {
            string Tags = e.Message.Text;
            foreach (Command command in commands)
            {
                if (Tags == command.Name)
                {
                    return;
                }
            }
            ApiClient api = new ApiClient();


            try
            {
                var result = await api.GetListDishesByCategory(Tags);
                SendInf(result, e.Message,Tags);
            }
            catch
            {
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Incorrectly entered information");
            }
            Bot.OnMessage -= GetString;




            EndComand();
        }

        protected async void SendInf(ListDish results, Message message, string s)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"Category {s} dishes\n\n" +
               $"1.{results.Results[0].Id} {results.Results[0].Name} \n\n" +
               $"2.{results.Results[1].Id} {results.Results[1].Name}\n\n" +
               $"3.{results.Results[2].Id} {results.Results[2].Name}\n\n" +
               $"4.{results.Results[3].Id} {results.Results[3].Name} \n\n" +
               $"5.{results.Results[4].Id} {results.Results[4].Name}\n\n" +
               $"6.{results.Results[5].Id} {results.Results[5].Name}\n\n" +
               $"7.{results.Results[6].Id} {results.Results[6].Name} \n\n" +
               $"8.{results.Results[7].Id} {results.Results[7].Name}\n\n" +
               $"9.{results.Results[8].Id} {results.Results[8].Name}\n\n" +
               $"10.{results.Results[9].Id} {results.Results[9].Name} \n\n", parseMode: ParseMode.Html);
        }



        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}