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
    class GetListCoctailsByCategory : Command
    { 

        public override bool IsWorking { get; set; } = false;
    public override TelegramBotClient Bot { get; set; }
    public override string Name { get; set; } = "/getlistcoctailsbycategory";

    public static List<Command> commands { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            Bot = _bot;
            await Bot.SendTextMessageAsync(e.From.Id, "Enter a category for the coctail");
            Bot.OnMessage+= GetString;
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
                var result = await api.GetListCoctailsByCategory(Tags);
                SendInf(result, e.Message,e.Message.Text);
            }
            catch
            {
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Incorrectly entered information");
            }
            Bot.OnMessage -= GetString;

           
        
       
        EndComand();
    }

    protected async void SendInf(ListCoctails results, Message message, string s)
    {
        await Bot.SendTextMessageAsync(message.From.Id, $"Category - {s} coctails\n\n" +
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
