namespace Core.Ollama;

public class OllamaInput
{
    public string model { get; set; }
    public OllamaMessage[] messages { get; set; }
    public bool stream { get; set; }
}

public class OllamaMessage
{
    public string role { get; set; }
    public string content { get; set; }
}

public class Message
{
    public string role { get; set; }
    public string content { get; set; }
}

public class OllamamResponse
{
    public string model { get; set; }
    public DateTime createdAt { get; set; }
    public Message message { get; set; }
    public string done_reason { get; set; }
    public bool done { get; set; }
    public long total_duration { get; set; }
    public long load_duration { get; set; }
    public long prompt_eval_duration { get; set; }
    public int eval_count { get; set; }
    public long eval_duration { get; set; }
}