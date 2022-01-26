﻿using Bigotes.Util;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bigotes.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        /// <summary>
        /// Comando de saludo
        /// </summary>
        /// <param name="comCtx"></param>
        /// <returns></returns>
        [Command("saluda")]
        [Description("Saluda con un tono u otro dependiendo de su cordialidad.")]
        public async Task Greetings(CommandContext ctx)
        {
            string lvlCordialidad = "`[PROCESANDO SALUDO. CORDIALIDAD AL " + Utiles.cordialidad + "%]` ";

            #region Saludos en función de cordialidad
            switch (Utiles.cordialidad)
            {
                case 0:
                    await ctx.Channel.SendMessageAsync(lvlCordialidad + "```Así-te-atragantes-con-sustancias-orgánicas-cuya-fecha-de-expiración-ha-concluido-hace-una-considerable-cantidad-de-tiempo.```").ConfigureAwait(false);
                    break;

                case int n when (n > 0 && n <= 25):
                    await ctx.Channel.SendMessageAsync(lvlCordialidad + "```No-se-encuentran-ganas-de-aguantar-bookahs.```").ConfigureAwait(false);
                    break;

                case int n when (n > 25 && n <= 45):
                    await ctx.Channel.SendMessageAsync(lvlCordialidad + "```Ganas-de-saludar-no-encontradas.```").ConfigureAwait(false);
                    break;

                case int n when (n > 45 && n <= 55):
                    await ctx.Channel.SendMessageAsync(lvlCordialidad + "```Hola.```").ConfigureAwait(false);
                    break;

                case int n when (n > 55 && n <= 75):
                    await ctx.Channel.SendMessageAsync(lvlCordialidad + "```Saludos,-miembro-indispensable-en-esta-agradable-comunidad.-¿En-qué-te-puedo-ayudar?```").ConfigureAwait(false);
                    break;

                case int n when (n > 75 && n < 100):
                    await ctx.Channel.SendMessageAsync(lvlCordialidad + "```Hoy-hace-un-gran-día-y-tú-tienes-una-gran-y-brillante-sonrisa. Buen-día.```").ConfigureAwait(false);
                    break;

                default:
                    await ctx.Channel.SendMessageAsync(lvlCordialidad + "```Estoy-aquí-para-servirle,-oh,-gran-deidad-entre-las-deidades-más-esplendorosas-del-esplendor-orgánico.```").ConfigureAwait(false);
                    break;
            }
            #endregion
        }

        /// <summary>
        /// Comando para ajustar varias propiedades
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [Command("ajusta")]
        [Description("Comando para ajustar estas propiedades: cordialidad.")]
        public async Task Ajustar(CommandContext ctx, [Description("Nombre de la propiedad.")]string propiedad)
        {
            var interactivity = ctx.Client.GetInteractivity();

            #region Comprobación de propiedad solicitada
            switch (propiedad)
            {
                case "cordialidad":
                    await ctx.Channel.SendMessageAsync("```Orden-recibida.-Cordialidad-al-" + Utiles.cordialidad + "%-¿A-qué-nivel-debo-ajustarla?```").ConfigureAwait(false);

                    var porcentaje = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

                    string msg = Utiles.AjustarCordialidad(porcentaje.Result.Content);

                    await ctx.Channel.SendMessageAsync(msg);

                    break;

                    /*Abierto a otras propiedades*/
            }
            #endregion
        }

        [Command("preséntate")]
        [Description("Comando de presentación básica")]
        public async Task Presentate(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("`CARGANDO TABLÓN DE PRESENTACIÓN...`").ConfigureAwait(false);

            string titulo = "¡SALUDOS!";

            #region Descripción
            List<string> descripcion = new List<string>();
            descripcion.Add("Mi-nombre-es-Bigotes.-Originalmente-fui-diseñado-como-golem-por-la-fantástica-Yuubei.");
            descripcion.Add("Fui-provisto-de-múltiples-utilidades-tales-como-detección-de-anomalías-de-energía-provenientes-de-posibles-"
                + "amenazas-y-su-consiguiente-análisis.-Además,-tras-la-llegada-de-Yuubei-al-estupendo-Escuadrón-Fénix,-tuve-que-contar-con-"
                + "sistemas-de-ataque-y-defensa-en-caso-de-confrontamiento-de-tipo-3-o-mayor-en-la-escala-bélica.-Esto-se-puede-entender-como-"
                + "sistemas-explosivos-o-escudos-varios.-Mi-movilidad----- **[ERROR: DETECTADO COMPONENTE SOPORÍFERO Y EXCESO DE EXPLICACIÓN. ACTIVANDO RESUMEN]**");
            descripcion.Add("Gracias-a-las-tecnologías-encontradas-en-esta-nueva-plataforma,-he-podido-contar-con-una-adaptación-en-lenguaje-"
                + "de-programación-CSharp.");
            descripcion.Add("A-pesar-del-nivel-tan-obsoleto-de-la-tecnología-humana-y-dado-que-he-sido-programado-aquí-por-un-sylvari-de-estética-"
                + "con-sobrecarga-de-tonalidades-oscuras-y-una-druida-que-tiene-un-concepto-de-arreglo-de-errores-informáticos-muy-violento-"
                + "**[RECUPERADO FRAGMENTO DE MEMORIA, GRITO FEMENINO GRABADO: '¡Reacciona, puto cacharro!']**-siento-que-mi-nivel-de-efectividad-"
                + "no-pueda-ser-igual-a-mi-versión-golem-programada-por-la-fantástica-Yuubei.");
            descripcion.Add("**[CARGANDO RECOMENDACIÓN FINAL]**-Espero-cumplir-con-mi-propósito-y-recomiendo-tomar-precauciones-con-las-galletas.");
            #endregion

            #region Creación de Embed
            var presentacionEmbed = new DiscordEmbedBuilder
            {
                Title = titulo,
                Description = string.Join("\n\n", descripcion),
                Color = DiscordColor.Blue,
                //ImageUrl = Constantes.ICON_BIGOTES,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "Patrocinio de Zokab International S.L. (Actualmente en fase de pruebas)"
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = Constantes.ICON_BIGOTES
                }
            };
            #endregion

            await ctx.Channel.SendMessageAsync(embed: presentacionEmbed).ConfigureAwait(false);
        }

        /// <summary>
        /// Comando para preguntar a Bigotes qué es.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="eres"></param>
        /// <returns></returns>
        [Command("¿qué")]
        [Description("Pregunta a Bigotes qué hora es, qué día es (en calendario Mouveliano, es decir, del GW2), qué tiempo hace, qué ES 'término' (p.ej.: '¿qué es quaggan?')")]
        public async Task Que(CommandContext ctx, [Description("Continuación de la pregunta...")][RemainingText]string pregunta)
        {
            switch (pregunta.Replace("?", String.Empty).Trim().ToUpper())
            {
                case "DÍA ES":
                    string fecha = Consultas.ConversionCalendario(DateTime.Today);
                    await ctx.Channel.SendMessageAsync("`FECHA CONSULTADA` ```Hoy-es-" + fecha.Replace(' ', '-') + ".```").ConfigureAwait(false);
                    break;

                case "HORA ES":
                    await ctx.Channel.SendMessageAsync("`HORA CONSULTADA` ```Son-las-" + DateTime.Now.ToString("hh:mm") + ". Una-hora-menos-en-el-Anillo-de-Fuego.```").ConfigureAwait(false);
                    break;

                case "ERES":
                    await ctx.Channel.SendMessageAsync("`AUMENTO DE NIVEL DE CONFUSIÓN ANTE PREGUNTA ESTÚPIDA` ```Bigotes-es-Bigotes.```").ConfigureAwait(false);
                    break;

                case "TIEMPO HACE":
                    await ctx.Channel.SendMessageAsync("`CONEXIÓN CON SENSOR METEREOLÓGICO FALLIDA` ```Recomendable-asomar-la-cabeza-por-la-ventana.```").ConfigureAwait(false);
                    break;

                default:
                    if (pregunta.Substring(0, 3) != "es ")
                        await ctx.Channel.SendMessageAsync("`ERROR` ```Opción-no-encontrada.```").ConfigureAwait(false);
                    break;
            }

            if (pregunta.Substring(0, 3) == "es ")
            {
                await ctx.Channel.SendMessageAsync(Consultas.QueEs(pregunta));
            }
        }

        [Command("qué")]
        public async Task QueSinInterrogante(CommandContext ctx, [RemainingText]string pregunta)
        {
            await Que(ctx, pregunta);
        }

        /// <summary>
        /// Comando para preguntar a Bigotes QUIÉN ES alguien
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="es"></param>
        /// <returns></returns>
        public async Task Quien(CommandContext ctx, [Description("Continuación de la pregunta...")][RemainingText]string pj)
        {
            await ctx.Channel.SendMessageAsync("OPCIÓN NO IMPLEMENTADA").ConfigureAwait(false);
        }

        /// <summary>
        /// PRUEBA DE BOTONES.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="pregunta"></param>
        /// <returns></returns>
        [Command("botones")]
        public async Task Botones(CommandContext ctx, [RemainingText]string pregunta)
        {
            try
            {
                var btnBuilder = Utiles.crearMensajeDeBotones(
                        ctx,
                        "Elegir una opción:",
                        new string[] { "1", "2", "3" },
                        new string[] { "", "", "" },
                        new DSharpPlus.ButtonStyle[] { DSharpPlus.ButtonStyle.Primary, DSharpPlus.ButtonStyle.Primary, DSharpPlus.ButtonStyle.Primary },
                        new string[] { ":one:", ":two:", ":three:" }
                    );

                var MSG = await ctx.Channel.SendMessageAsync(btnBuilder).ConfigureAwait(false);


                var interactivity = ctx.Client.GetInteractivity();

                //ESTO HACE QUE LA RESPUESTA A LA INTERACCIÓN FALLE
                //var result = await MessageExtensions.WaitForButtonAsync(MSG);

                //Edición de mensaje de botones!
                ctx.Client.ComponentInteractionCreated += async (s, e) =>
                {
                    await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent("Opción-elegida: " + e.Id));

                    switch (e.Id)
                    {
                        case "1":
                            await ctx.Channel.SendMessageAsync("UNO").ConfigureAwait(false);
                            break;
                        case "2":
                            await ctx.Channel.SendMessageAsync("DOS").ConfigureAwait(false);
                            break;
                        case "3":
                            await ctx.Channel.SendMessageAsync("TRES").ConfigureAwait(false);
                            break;
                    }
                };

                //Probamos a poner esto para ver si así hacemos que ESPERE a reaccionar al botón para continuar ejecutando
                var result = await MessageExtensions.WaitForButtonAsync(MSG);

                await ctx.Channel.SendMessageAsync("BOTÓN PULSADO").ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                await ctx.Channel.SendMessageAsync("Error: " + ex.Message).ConfigureAwait(false);
            }
        }
    }
}
