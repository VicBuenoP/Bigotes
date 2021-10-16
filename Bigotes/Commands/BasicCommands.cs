﻿using Bigotes.Util;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string lvlCordialidad = "`[PROCESANDO SALUDO. CORDIALIDAD AL " + Properties.cordialidad + "%]` ";

            #region Saludos en función de cordialidad
            switch (Properties.cordialidad)
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

                case 100:
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
                    await ctx.Channel.SendMessageAsync("```Orden-recibida.-Cordialidad-al-" + Properties.cordialidad + "%-¿A-qué-nivel-debo-ajustarla?```").ConfigureAwait(false);

                    var porcentaje = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

                    if(!Regex.IsMatch(porcentaje.Result.Content.Trim(), @"\d%"))
                    {
                        await ctx.Channel.SendMessageAsync("`[CARGANDO MENSAJE DE ERROR]` ```Error-en-formato.``` `[CARGANDO SUGERENCIA]` ```Usar-formato-00%.```");
                    }
                    else
                    {
                        int nivel = Int32.Parse(porcentaje.Result.Content.Trim().Split('%')[0]);

                        Properties.cordialidad = nivel;

                        await ctx.Channel.SendMessageAsync("`[CARGANDO MENSAJE DE CONFIRMACIÓN]` ```Ajustada-cordialidad-al-" + nivel + "%.```");
                    }

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

            var presentacionEmbed = new DiscordEmbedBuilder
            {
                Title = titulo,
                Description = string.Join("\n\n", descripcion),
                Color = DiscordColor.Blue
                //ImageUrl = "../Content/Gifs/Bigotes_salute.gif"
            };

            await ctx.Channel.SendMessageAsync(embed: presentacionEmbed).ConfigureAwait(false);
        }

        /// <summary>
        /// Comando para preguntar a Bigotes qué es. Te dirá una realidad sorprendente.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="eres"></param>
        /// <returns></returns>
        [Command("¿qué")]
        [Description("Pregunta a Bigotes qué es. Te desvelará una realidad sorprendente.")]
        public async Task QueEres(CommandContext ctx, [Description("Importante que sea un '¿qué eres?'")]string eres)
        {
            if (eres.Trim().ToUpper() == "ERES?")
            {
                await ctx.Channel.SendMessageAsync("`AUMENTO DE NIVEL DE CONFUSIÓN ANTE PREGUNTA ESTÚPIDA` ```Bigotes-es-Bigotes.```").ConfigureAwait(false);
            }
        }
    }
}
