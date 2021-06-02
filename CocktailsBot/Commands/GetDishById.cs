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
    class GetDishById : Command
    {
        public override bool IsWorking { get; set; } = false;
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/getdishbyid";

        public static List<Command> commands { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            Bot = _bot;
            await Bot.SendTextMessageAsync(e.From.Id, "Enter a category for a cocktail");
            Bot.OnMessage += GetString;
        }
        private async void GetString(object sender, MessageEventArgs e)
        {
            string Id = e.Message.Text;
            foreach (Command command in commands)
            {
                if (Id == command.Name)
                {
                    return;
                }
            }
            ApiClient api = new ApiClient();


            try
            {
                var result = await api.GetDish(Id);
                SendInf(result, e.Message);
            }
            catch
            {
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Incorrectly entered information");
            }
            Bot.OnMessage -= GetString;




            EndComand();
        }

        protected async void SendInf(Dishes results, Message message)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"{results.Name} - name dish, Id" +
                $" which you entered\n\n", parseMode: ParseMode.Html);
        }



        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
