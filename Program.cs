using Newtonsoft.Json;
using SignalMessageDumper.JsonTypes;
using SignalMessageDumper.SqlTypes;
using SQLite;
using System.CommandLine;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SignalMessageDumper
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("Dump Signal Desktop messages");

            var pathArgument = new Option<string>(
                name: "--rootPath",
                description: "Path to Signal folder")
                    { IsRequired = true };

            var phoneNumberOption = new Option<string>(
                name: "--number",
                description: "Specific phone number to dump");

            rootCommand.Add(pathArgument);
            rootCommand.AddOption(phoneNumberOption);

            rootCommand.SetHandler((inputPath, phoneNumber) =>
            {
                DumpMessages(inputPath, phoneNumber);
            },
            pathArgument, phoneNumberOption);

            return await rootCommand.InvokeAsync(args);
        }

        private static void DumpMessages(string inputPath, string phoneNumber)
        {
            var dbPath = Path.Combine(inputPath, "sql\\db.sqlite");
            var configPath = Path.Combine(inputPath, "config.json");

            var key = GetKey(configPath);

            var options = new SQLiteConnectionString(dbPath, true, key: key);
            var connection = new SQLiteConnection(options);

            //var tables = connection.Query<table>("SELECT * FROM sqlite_master where type='table';");
            //foreach (var table in tables)
            //{
            //    Console.WriteLine(table.Name);
            //    Console.WriteLine(table.SQL);
            //    Console.WriteLine();
            //}

            var conversations = connection.Query<ConversationInfo>("SELECT * FROM conversations");
            var bucketed = conversations.GroupBy(c => c.type).ToDictionary(g => g.Key, g => g.ToList());

            var messages = connection.Query<Message>("SELECT * FROM messages");
            var messageTypes = messages.Select(a => a.type).Distinct().ToList();

            foreach (var convo in bucketed["private"])
            {
                var sb = new StringBuilder();

                sb.Append($"ProfileFullName: {convo.profileFullName}<br/>");
                sb.Append($"ProfileName: {convo.profileName}<br/>");
                sb.Append($"Name: {convo.name}<br/>");

                sb.Append("<table>");
                sb.Append("<tr>" +
                        "<th>Source</th>" +
                        "<th>Sent At</th>" +
                        "<th>Type</th>" +
                        "<th>Source Device</th>" +
                        "<th>Body</th>" +
                    "</tr>");

                var convoMessages = messages.Where(m => m.conversationId == convo.Id)
                    .OrderBy(m => m.sent_at);

                foreach (var convoMessage in convoMessages)
                {
                    //TODO: Finish processing attachments
                    //if(convoMessage.hasAttachments == 1)
                    //{
                    //    var attachmentInfo = JsonConvert.DeserializeObject<MessageJson>(convoMessage.json);
                        
                    //    var attachment = attachmentInfo.attachments.FirstOrDefault();
                    //    if( attachment != null) {
                    //        Console.WriteLine(attachment.fileName);
                    //    }
                    //    else
                    //    {
                    //        sb.Append(convoMessage.isErased + "<br/>");
                    //    }
                    //}

                    var body = convoMessage.body;
                    if (body == null)
                    {
                        body = $"isViewOnce:{convoMessage.isViewOnce} isErased:{convoMessage.isErased}";
                    }

                    sb.Append($"<tr>" +
                            $"<td>{convoMessage.source}</td>" +
                            $"<td>{UnixTime.DateTimeFromUnixTimestampMillis(convoMessage.sent_at)}</td>" +
                            $"<td>{convoMessage.type}</td>" +
                            $"<td>{convoMessage.sourceDevice}</td>" +
                            $"<td>{body}</td>" +
                        $"</tr>");
                }
                sb.Append("</table>");

                File.WriteAllText(convo.e164 + ".html", sb.ToString());
            }
        }

        private static byte[] GetKey(string configPath)
        {
            var fileConents = System.IO.File.ReadAllText(configPath);
            var key = JsonConvert.DeserializeObject<KeyHolder>(fileConents).key;
            var returnList = new List<byte>();
            for (int x = 0; x < key.Length; x += 2)
            {
                returnList.Add(Convert.ToByte(key.Substring(x, 2), 16));
            }

            return returnList.ToArray();
        }
    }
}