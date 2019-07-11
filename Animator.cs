namespace stick_man
{
    public class Animator {
        
        double leftUpperLeg_o_x, leftUpperLeg_o_y, leftUpperLeg_f_x, leftUpperLeg_f_y = 0; 
        double leftLowerLeg_o_x, leftLowerLeg_o_y, leftLowerLeg_f_x, leftLowerLeg_f_y = 0; 
        double rightUpperLeg_o_x, rightUpperLeg_o_y, rightUpperLeg_f_x, rightUpperLeg_f_y = 0; 
        double rightLowerLeg_o_x, rightLowerLeg_o_y, rightLowerLeg_f_x, rightLowerLeg_f_y = 0;

        double leftUpperArm_o_x, leftUpperArm_o_y, leftUpperArm_f_x, leftUpperArm_f_y = 0; 
        double leftLowerArm_o_x, leftLowerArm_o_y, leftLowerArm_f_x, leftLowerArm_f_y = 0; 
        double rightUpperArm_o_x, rightUpperArm_o_y, rightUpperArm_f_x, rightUpperArm_f_y = 0; 
        double rightLowerArm_o_x, rightLowerArm_o_y, rightLowerArm_f_x, rightLowerArm_f_y = 0; 

        public void Walk_Front() {
            rightLowerArm_o_x = 5;
            rightLowerArm_f_x = 5;
            rightLowerArm_f_y = 20;

            leftLowerArm_f_x = 20;
            leftLowerArm_f_y = 5;

            leftLowerLeg_f_x = -5;
            leftLowerLeg_f_y = 15;

            rightUpperLeg_f_x = 5;
            rightUpperLeg_f_y = 10;

            rightLowerLeg_o_x = 5;
            rightLowerLeg_o_y = 10;
            rightLowerLeg_f_x = 10;
            rightLowerLeg_f_y = 5;
        }

        public void Walk_Normal() {
            rightLowerArm_o_x = 0;
            rightLowerArm_f_x = 0;
            rightLowerArm_f_y = 0;

            leftLowerArm_f_x = 0;
            leftLowerArm_f_y = 0;

            leftLowerLeg_f_x = 0;
            leftLowerLeg_f_y = 0;

            rightUpperLeg_f_x = 0;
            rightUpperLeg_f_y = 0;

            rightLowerLeg_o_x = 0;
            rightLowerLeg_o_y = 0;
            rightLowerLeg_f_x = 0;
            rightLowerLeg_f_y = 0;
        }

        public Ponto4D[] Animate(Limb limb) {
            switch(limb.GetBodyPart()) {
                case BodyPart.LEFT_UPPER_LEG:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+leftUpperLeg_o_x, limb.GetRoot().Y+leftUpperLeg_o_y),
                        new Ponto4D(limb.GetFinal().X+leftUpperLeg_f_x, limb.GetFinal().Y+leftUpperLeg_f_y)
                    };
                case BodyPart.LEFT_LOWER_LEG:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+leftLowerLeg_o_x, limb.GetRoot().Y+leftLowerLeg_o_y),
                        new Ponto4D(limb.GetFinal().X+leftLowerLeg_f_x, limb.GetFinal().Y+leftLowerLeg_f_y)
                    };
                case BodyPart.RIGHT_UPPER_LEG:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+rightUpperLeg_o_x, limb.GetRoot().Y+rightUpperLeg_o_y),
                        new Ponto4D(limb.GetFinal().X+rightUpperLeg_f_x, limb.GetFinal().Y+rightUpperLeg_f_y)
                    };
                case BodyPart.RIGHT_LOWER_LEG:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+rightLowerLeg_o_x, limb.GetRoot().Y+rightLowerLeg_o_y),
                        new Ponto4D(limb.GetFinal().X+rightLowerLeg_f_x, limb.GetFinal().Y+rightLowerLeg_f_y)
                    };
                case BodyPart.LEFT_UPPER_ARM:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+leftUpperArm_o_x, limb.GetRoot().Y+leftUpperArm_o_y),
                        new Ponto4D(limb.GetFinal().X+leftUpperArm_f_x, limb.GetFinal().Y+leftUpperArm_f_y)
                    };
                case BodyPart.LEFT_LOWER_ARM:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+leftLowerArm_o_x, limb.GetRoot().Y+leftLowerArm_o_y),
                        new Ponto4D(limb.GetFinal().X+leftLowerArm_f_x, limb.GetFinal().Y+leftLowerArm_f_y)
                    };
                case BodyPart.RIGHT_UPPER_ARM:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+rightUpperArm_o_x, limb.GetRoot().Y+rightUpperArm_o_y),
                        new Ponto4D(limb.GetFinal().X+rightUpperArm_f_x, limb.GetFinal().Y+rightUpperArm_f_y)
                    };
                case BodyPart.RIGHT_LOWER_ARM:
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X+rightLowerArm_o_x, limb.GetRoot().Y+rightLowerArm_o_y),
                        new Ponto4D(limb.GetFinal().X+rightLowerArm_f_x, limb.GetFinal().Y+rightLowerArm_f_y)
                    };
                default: 
                    return new Ponto4D[]{
                        new Ponto4D(limb.GetRoot().X, limb.GetRoot().Y),
                        new Ponto4D(limb.GetFinal().X, limb.GetFinal().Y)
                    };
            }
        }

    }
}