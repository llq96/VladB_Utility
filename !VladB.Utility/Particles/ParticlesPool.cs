namespace VladB.Utility {
    public class ParticlesPool : SimplePool {
        public Particle GetFreeParticle() => GetFreeObjectAs<Particle>();
    }
}


