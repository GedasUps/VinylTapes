using SmartComponents.LocalEmbeddings;
using Pgvector;


namespace VinylTapes.Services
{
    public class VectorService: IDisposable
    {
        private readonly LocalEmbedder _embedder;

        public VectorService()
        {
            // Modelis užkraunamas į RAM vieną kartą (apie 100-200 MB)
            _embedder = new LocalEmbedder();
        }

        public Vector GenerateVector(string text)
        {
            // Sugeneruojame embedding'ą iš teksto
            var embedding = _embedder.Embed(text);

            // Paverčiame į pgvector formatą (float masyvą)
            return new Vector(embedding.Values.ToArray());
        }

        public void Dispose() => _embedder.Dispose();
    }
}
