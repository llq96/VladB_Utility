namespace VladB.Utility {
    public class ParticlesPool : SimplePool {
        public Particle GetFreeParticle() {
            IPoolObject obj = GetFreeObject();
            return (obj != null) ? (obj as Particle) : null;
        }
    }
}


