using System.Collections.Generic;
using VladB.Utility;

namespace VladB.GameTemplate {
    public class ItemGrabbingAction_Particles : ItemGrabbingAction {
        public List<Particle> particles;
        public override void DoAction() {
            particles.Act(particle => particle.TryDo(p => p.Play()));
        }
    }
}