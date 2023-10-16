using System.Text;
using System.Text.RegularExpressions;

var textFromFile = new StringBuilder();

try
{
    var filePath = "../../../../fileTORead.txt";

    if (File.Exists(filePath))
    {
        textFromFile.Append(File.ReadAllText(filePath));
        Console.WriteLine(textFromFile);
    }
    else
    {
        Console.WriteLine("Файл не найден");
    }
}
catch (Exception e)
{
    Console.WriteLine("ERROR: " + e.Message);
}

Console.WriteLine();
var defaultCount = 0;


while (true)
{
    Console.WriteLine("Выбери, what do you want:" +
        "\n1. Найти слова, содержащие максимальное количество цифр" +
        "\n2. Найти самое длинное слово и определить, сколько раз оно встретилось в тексте" +
        "\n3. Заменить цифры от 0 до 9 на слова «ноль», «один», ..., «девять»" +
        "\n4. Вывести на экран сначала вопросительные, а затем восклицательные предложения" +
        "\n5. Вывести на экран только предложения, не содержащие запятых" +
        "\n6. Найти слова, начинающиеся и заканчивающиеся на одну и ту же букву" +
        "\nq. Выход (нажмите \"q\" или \"й\")");

    var selectedOption = Console.ReadLine();
    if (selectedOption == "q" || selectedOption == "й")
    {
        return;
    }

    var text = textFromFile.ToString();
    var words = text.Split(' ');
    var maxWord = "";
    var moreThenOne = false;


    switch (selectedOption)
    {
        case "1":
            var maxDigitCount = 0;

            foreach (string word in words)
            {
                var digitCount = word.Count(char.IsDigit);
                if (digitCount > maxDigitCount)
                {
                    maxDigitCount = digitCount;
                    maxWord = word;
                }
            }
            Console.WriteLine($"Что-то похожее на слово имеет максимальное количество цифр в количестве " +
                $"{maxDigitCount} единиц и имеет следующий вид =====>");

            foreach (string word in words)
            {
                var digitCount = word.Count(char.IsDigit);
                if (digitCount == maxDigitCount)
                {
                    Console.WriteLine($"=====> {word}");
                }
            }
            break;

        case "2":
            var maxletterOrDigitLength = 0;
            var wordCounts = new Dictionary<string, int>();

            foreach (string word in words)
            {
                var letterOrDigitCount = word.Count(char.IsLetterOrDigit);

                if (letterOrDigitCount > maxletterOrDigitLength)
                {
                    maxletterOrDigitLength = letterOrDigitCount;
                    wordCounts.Clear();
                    wordCounts.Add(word, 1);
                }
                else if (letterOrDigitCount == maxletterOrDigitLength)
                {
                    if (!wordCounts.ContainsKey(word))
                    {
                        wordCounts.Add(word, 1);
                    }
                    else
                    {
                        wordCounts[word]++;
                    }
                }
            }
            Console.WriteLine($"Слова с максимальной длиной {maxletterOrDigitLength} символов:");
            foreach (var wordCount in wordCounts)
            {
                Console.WriteLine($"=====> {wordCount.Key} (повторяется {wordCount.Value} раз)");
            }
            break;

        case "3":
            var numberToWord = textFromFile.Replace("0", "НОЛЬ").Replace("1", "ОДИН").Replace("2", "ДВА")
                .Replace("3", "ТРИ").Replace("4", "ЧЕТЫРЕ").Replace("5", "ПЯТЬ").Replace("6", "ШЕСТЬ")
                .Replace("7", "СЕМЬ").Replace("8", "ВОСЕМЬ").Replace("9", "ДЕВЯТЬ");
            Console.WriteLine(numberToWord);
            break;
        case "4":
            //var sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Select(sentence => sentence.Trim());
            var sentences = Regex.Split(text, @"(?<!\w\.\w)(?<![A-Z][a-z]\.)(?<=\.|\?|\!)\s");
            var questionSentences = sentences.Where(sentence => sentence.EndsWith("?")).ToList();
            var exclamationSentences = sentences.Where(sentence => sentence.EndsWith("!")).ToList();


            Console.WriteLine("Восклицательные предложения:");
            foreach (var sentence in exclamationSentences)
            {
                Console.WriteLine(sentence);
            }
            Console.WriteLine("Вопросительные предложения:");
            foreach (var sentence in questionSentences)
            {
                Console.WriteLine(sentence);
            }

            break;

        default:
            moreThenOne = true;
            defaultCount++;
            break;
    }
    if (moreThenOne)
    {
        if (defaultCount == 1)
        {
            Console.WriteLine("Ты ввёл что-то не то. Попробуй воспользоваться цифрой от 1 до 6");
        }
        else if (defaultCount > 1)
        {
            Console.WriteLine($"Ты бы прекращал. Ввёл какую-то ерунду уже {defaultCount} раз");
        }
    }
}