using ICSharpCode.Decompiler.CSharp.Syntax.PatternMatching;
using System.Net.Http;
using WebSite.Models.ChatGpt;

namespace WebSite.Services
{
    public class ChatGptService
    {
        private HttpClient _httpClient = new HttpClient();
        private string _apiKey = "sk-95Ez2Z1rtgegrRTFFDSTVTdsdfsgdv3422kjhLghnh53QiT8F";
        private string _endpoint = "http://192.168.56.1:1337/v1/chat/completions";

        public async Task<string> Request(string content)
        {
            var messages = new List<Message>();
            var message = new Message() { Role = "user", Content = content };
            // добавляем сообщение в список сообщений
            messages.Add(message);

            // формируем отправляемые данные
            var requestData = new Request()
            {
                ModelId = "gpt-3.5-turbo",
                Messages = messages
            };

            // отправляем запрос
            using var response = await _httpClient.PostAsJsonAsync(_endpoint, requestData);

            // если произошла ошибка, выводим сообщение об ошибке на консоль
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
                return "";
            }
            // получаем данные ответа
            var responseData = await response.Content.ReadFromJsonAsync<ResponseData>();

            var choices = responseData?.choices;

            if (choices.Count == 0)
            {
                Console.WriteLine("No choices were returned by the API");
                return "";
            }

            var choice = choices[0];
            var responseMessage = choice.message;
            // добавляем полученное сообщение в список сообщений
            messages.Add(responseMessage);
            var responseText = responseMessage.Content.Trim();

            return responseText;
        }
    }
}
