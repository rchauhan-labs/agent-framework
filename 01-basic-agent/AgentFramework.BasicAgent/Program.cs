using DotNetEnv;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

Env.Load();
var credential = System.Environment.GetEnvironmentVariable("GITHUB_TOKEN");

IChatClient chatClient = new ChatClient(
            "openai/gpt-4o-mini",
            new ApiKeyCredential(credential),
            new OpenAIClientOptions { Endpoint = new Uri("https://models.github.ai/inference") }
            )
            .AsIChatClient();

AIAgent spellCheckerAgent = new ChatClientAgent(chatClient,
    new ChatClientAgentOptions
    {
        Name = "spellCheckerAgent",
        Instructions = "You are a helpful spell checker assistant. " +
        "For the input data, identify and fix any spelling errors"
    });

Console.Write("You : " );
string userInput = Console.ReadLine() ?? string.Empty;

Console.ForegroundColor = ConsoleColor.Green;

string finalResponse = String.Empty;
var result = await spellCheckerAgent.RunAsync(userInput);

Console.WriteLine("SpellChecker Agent Output : " + result.Text);
