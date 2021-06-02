using WebApplication1;
using CocktailsBot.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace CocktailsBot.Commands
{
    public abstract class Command
    {
        public abstract TelegramBotClient Bot { get; set; }
        public abstract bool IsWorking { get; set; }
        private readonly ApiClient _cocktailsClient = new ApiClient();
        public abstract string Name { get; set; }
        public abstract void Execute(TelegramBotClient _bot, Message e, List<Command> _commands);
        public abstract void EndComand();
      
    }
}
