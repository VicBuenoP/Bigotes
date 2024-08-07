﻿using Bigotes.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using DSharpPlus.Entities;
using Bigotes.Clases;
using SpotifyAPI.Web;
using DSharpPlus.Interactivity.Enums;

namespace Bigotes
{
    public class Bot
    {
        #region Propiedades
        /// <summary>
        /// Cliente de Discord
        /// </summary>
        public DiscordClient Client { get; private set; }

        /// <summary>
        /// Extensión de interactividad
        /// </summary>
        public InteractivityExtension Interactivity { get; private set; }

        /// <summary>
        /// Extensión de comandos
        /// </summary>
        public CommandsNextExtension Commands { get; private set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método asíncrono para iniciar el cliente
        /// con la configuración completa.
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync()
        {
            try
            {
                #region Obtención de fichero de configuración (JSON)
                string jsonSTR = string.Empty;

                using (var fs = File.OpenRead("config.json"))
                {
                    using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    {
                        jsonSTR = await sr.ReadToEndAsync().ConfigureAwait(false);
                    }
                }

                Util.Utiles.ConfigJson = JsonConvert.DeserializeObject<ConfigJSON>(jsonSTR);
                #endregion

                #region Configuración de bot
                DiscordConfiguration config = new DiscordConfiguration
                {
                    Token = Util.Utiles.ConfigJson.Token,
                    TokenType = TokenType.Bot,
                    AutoReconnect = true,
                    MinimumLogLevel = LogLevel.Debug
                };

                Client = new DiscordClient(config);
                #endregion

#region Configuración de Lavalink
#if LAVALINK
                var endpoint = new ConnectionEndpoint
                {
                    Hostname = "127.0.0.1",
                    Port = 2333
                };

                var lavalinkConfig = new LavalinkConfiguration
                {
                    Password = "LavaHostias",
                    RestEndpoint = endpoint,
                    SocketEndpoint = endpoint
                };

                var lavalink = Client.UseLavalink();
#endif
#endregion

                

                Client.Ready += OnClientReady;

                //await ctx.Channel.SendMessageAsync("`[ACTIVADO PROTOCOLO BOT-DE-BIGOTES]` ```Funciones-principales-activadas.-Bot-a-la-escucha.```");

                #region Interactividad con el canal (leer, interactuar)
                Client.UseInteractivity(new InteractivityConfiguration
                {
                    PollBehaviour = PollBehaviour.KeepEmojis,
                    Timeout = TimeSpan.FromMinutes(30), //Tiempo de espera de interacciones (leer mensajes, p.ej.) HAY LÍMITE
                    ResponseBehavior =InteractionResponseBehavior.Ack
                });
                #endregion

                #region Comandos
                CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
                {
                    StringPrefixes = new string[] { Util.Utiles.ConfigJson.Prefix },
                    EnableDms = false,
                    EnableMentionPrefix = true,
                    IgnoreExtraArguments = true,
                    EnableDefaultHelp = false //<- ASEGURARSE DE TENER COMANDO DE AYUDA PROPIO
                };

                Commands = Client.UseCommandsNext(commandsConfig);

                #region Registro de comandos
                Commands.RegisterCommands<BasicCommands>();
                Commands.RegisterCommands<DiceCommands>();
                Commands.RegisterCommands<CreationCommands>();
                Commands.RegisterCommands<MusicCommands>();
                #endregion

                #endregion

                await Client.ConnectAsync(new DiscordActivity("'Bigotes, ayuda'", ActivityType.ListeningTo));

                //Conexión a lavalink
#if LAVALINK
                await lavalink.ConnectAsync(lavalinkConfig);
#endif

                //Tiempo de espera extra para dar tiempo a procesamiento de peticiones
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método asíncrono que se inicializa cuando
        /// el cliente está preparado.
        /// </summary>
        /// <param name="s">Este parámetro ha sido añadido en los últimos parches del Nugget</param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Task OnClientReady(DiscordClient s, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
#endregion
    }
}
