using Godot;
using System.Collections.Generic;

public partial class Part : Node
{
    [Export] public User MyUser;
    [Export] public Sprite2D Display;
    [Export] public float MaxDurability = 100;
    [Export] public float Durability = 1000;
    [Export] public float MaxMaxElectricity = 1000;
    [Export] public float MaxElectricity = 1000;
    [Export] public float Electricity = 0;
    [Export] public float MaxIntake = 500;
    [Export] public float Intake = 500;
    [Export] public float MaxOutput = 1000;
    [Export] public float Output = 1000;
    [Export] public float MaxHeat;
    [Export] public float Heat;
    [Export] public float MaxBurn;
    [Export] public float Burn;
    [Export] public Vector2 WireOffset;
    [Export] public ThinkingObject Mind;

    [Export]
    public Godot.Collections.Dictionary<string, long> IntFlags = new Godot.Collections.Dictionary<string, long>
    {

    };
    [Export]
    public Godot.Collections.Dictionary<string, float> FloatFlags = new Godot.Collections.Dictionary<string, float>
    {

    };
    [Export]
    public Godot.Collections.Dictionary<string, bool> BoolFlags = new Godot.Collections.Dictionary<string, bool>
    {

    };
    [Export]
    public Godot.Collections.Dictionary<string, string> StringFlags = new Godot.Collections.Dictionary<string, string>
    {

    };
    [Export]
    public Godot.Collections.Dictionary<string, long> ShortTermIntFlags = new Godot.Collections.Dictionary<string, long>
    {

    };
    [Export]
    public Godot.Collections.Dictionary<string, float> ShortTermFloatFlags = new Godot.Collections.Dictionary<string, float>
    {

    };
    [Export]
    public Godot.Collections.Dictionary<string, bool> ShortTermBoolFlags = new Godot.Collections.Dictionary<string, bool>
    {

    };
    [Export]
    public Godot.Collections.Dictionary<string, string> ShortTermStringFlags = new Godot.Collections.Dictionary<string, string>
    {

    };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FindUser(this);
        base._Ready();
    }

    public class BrainWord
    {
        public bool SanatizePass;
        private BrainWord()
        {

        }

        public class Flag : BrainWord
        {
            private Flag()
            {

            }

            public Value.FlagReference Destination;

            public class Value : BrainWord
            {
                private Value()
                {

                }

                public FlagType DestinationType;

                public class FlagReference : Value
                {
                    public bool DestinationIsShortTerm = true;
                    public string Destination;

                    public FlagReference(bool TargetIsShortTerm, FlagType TargetFlagType, string Target)
                    {
                        DestinationIsShortTerm = TargetIsShortTerm;
                        DestinationType = TargetFlagType;
                        Destination = Target;
                    }
                }

                public class Constant : Value
                {
                    public string Value;

                    public Constant(FlagType TargetFlagType, string Target)
                    {
                        DestinationType = TargetFlagType;
                        Value = Target;
                    }
                }
                public class RandomValue : Value
                {
                    public Value Minimum;
                    public Value Maximum;

                    public RandomValue(FlagType TargetFlagType, Value Min, Value Max)
                    {
                        DestinationType = TargetFlagType;
                        Minimum = Min;
                        Maximum = Max;
                    }
                }
            }

            // public class GetVariable : Flag
            // {
            // 	private GetVariable()
            // 	{

            // 	}

            // 	public class GetState : GetVariable
            // 	{
            // 		public GetState(Value.FlagReference DestinationFlag, VariableTarget TargetVariable)
            // 		{
            // 			Destination = DestinationFlag;
            // 			Target = TargetVariable;
            // 		}
            // 		VariableTarget Target;

            // 		public enum VariableTarget
            // 		{
            // 			// IntFlagShortTerm,
            // 			// FloatFlagShortTerm,
            // 			// BoolFlagShortTerm,
            // 			// StringFlagShortTerm,

            // 			// IntFlagLongTerm,
            // 			// FloatFlagLongTerm,
            // 			// BoolFlagLongTerm,
            // 			// StringFlagLongTerm,

            // 			IntLife,
            // 			IntStomach,
            // 			IntApples,
            // 			IntMoney,
            // 			IntSleep,
            // 			IntAge,

            // 			FloatExertion,

            // 			BoolWorking,
            // 			BoolAsleep,
            // 			BoolAtTarget,

            // 			StringName,
            // 		}
            // 	}


            // }

            public class Math : Flag
            {
                public Value Input;
                public Operation SelectedOperation;

                public Math(Value.FlagReference DestinationFlag, Value InputFlag, Operation TargetOperation)
                {
                    Destination = DestinationFlag;
                    Input = InputFlag;
                    SelectedOperation = TargetOperation;
                }
                public enum Operation
                {
                    Set,
                    Add,
                    Subtract,
                    multiply,
                    Divide,
                    Modulus,
                }
            }
            public class ReadFlag : Flag
            {
                public ReadFlag(Value.FlagReference Read, Operation TargetOperation, Value CompareValue)
                {
                    Destination = Read;
                    SelectedOperation = TargetOperation;
                    Input = CompareValue;
                }
                public Operation SelectedOperation;
                public Value Input;
                public enum Operation
                {
                    FlagInintialized,
                    FlagUnInintialized,
                    FlagEquals,
                    FlagGreaterThan,
                    FlagLessThan,
                }
            }

            public enum FlagType
            {
                IntFlag,
                FloatFlag,
                BoolFlag,
                StringFlag,
            }
        }

        public class Action : BrainWord
        {
            private Action()
            {

            }
            public class Pass(Flag.Value Direction, bool ToSanatizeOrNotToSanatize) : Action
            {
                public Flag.Value PassId = Direction;
                public bool Sanatize = ToSanatizeOrNotToSanatize;
            }

            public class Pull(Flag.Value.FlagReference Direction, Flag.Value.FlagReference OutputPoint) : Action
            {
                public Flag.Value PullId = Direction;
                public Flag.Value.FlagReference Output = OutputPoint;
            }

            public class Print(Flag.Value WhatToPrint) : Action
            {
                public Flag.Value ToPrint = WhatToPrint;
            }

            public class PartSettings(Flag.Value Setting, Flag.Value SettingValue, Flag.Math.Operation Method) : Action
            {
                public Flag.Value Target = Setting;
                public Flag.Value Input = SettingValue;
                public Flag.Math.Operation Operation = Method;
            }

            public class GetStatus : Action
            {
                public Flag.Value.FlagReference Output;
                public string Input;

                public GetStatus(Flag.Value.FlagReference Target, string TargetInput)
                {
                    Output = Target;
                    Input = TargetInput;
                }
            }

        }

        public class Else : BrainWord
        {

        }

        public class CheckInput(string ToDetect, CheckInput.InputType TypeToDetect) : BrainWord

        {
            public string Target = ToDetect;
            public InputType WhichType = TypeToDetect;
            public enum InputType
            {
                Pressed,
                JustPressed,
                Released,
            }
        }
    }

    public struct Behaviour
    {
        public Behaviour(List<List<BrainWord>> Job)
        {
            Task = Job;
        }
        public List<List<BrainWord>> Task;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
    }
    public override void _PhysicsProcess(double delta)
    {
        Durability = Mathf.Clamp(Durability, 0, MaxDurability);
        MaxElectricity = Mathf.Clamp(MaxElectricity, 0, MaxMaxElectricity);
        Electricity = Mathf.Clamp(Electricity, 0, MaxElectricity);
        Intake = Mathf.Clamp(Intake, 0, MaxIntake);
        Output = Mathf.Clamp(Intake, 0, MaxOutput);
        Burn = Mathf.Clamp(Burn, 0, MaxBurn);
        base._PhysicsProcess(delta);
        if (Mind != null)
        {
            RunCommands(Mind.CommandSet, this);
        }
    }
    public void FindUser(Node StartPoint)
    {
        if (StartPoint is User)
        {
            MyUser = StartPoint as User;
        }
        else if (StartPoint != GetTree().Root)
        {
            FindUser(StartPoint.GetParent());
        }
    }

    public void RunCommands(List<Behaviour> Brain, Part FlagHolder = null)
    {
        if (this is not DataLine)
        {
            if (Brain != null)
            {
                if (FlagHolder == null)
                {
                    FlagHolder = this;
                }
                ShortTermIntFlags = [];
                ShortTermFloatFlags = [];
                ShortTermBoolFlags = [];
                ShortTermStringFlags = [];
                for (int CurrentBehaviour = 0; CurrentBehaviour < Brain.Count; CurrentBehaviour++)
                {
                    for (int CurrentBrainSentence = 0; CurrentBrainSentence < Brain[CurrentBehaviour].Task.Count; CurrentBrainSentence++)
                    {
                        bool SentanceSkiped = false;
                        bool Passed = false;
                        for (int WordId = 0; WordId < Brain[CurrentBehaviour].Task[CurrentBrainSentence].Count; WordId++)
                        {
                            if (Passed == false)
                            {
                                if (Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.Else)
                                {
                                    SentanceSkiped = !SentanceSkiped;
                                }
                                else if (SentanceSkiped == false)
                                {
                                    try
                                    {
                                        if (Mind is Core && Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.CheckInput)
                                        {
                                            switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.CheckInput).WhichType)
                                            {
                                                case BrainWord.CheckInput.InputType.Pressed:
                                                    if (Mind is Core)

                                                        if (!(Mind as Core).Inputs.Contains((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.CheckInput).Target))
                                                        {
                                                            SentanceSkiped = true;
                                                        }
                                                    break;
                                                case BrainWord.CheckInput.InputType.JustPressed:
                                                    if (!(Mind as Core).Inputs.Contains((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.CheckInput).Target) || (Mind as Core).OldInputs.Contains((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.CheckInput).Target))
                                                    {
                                                        SentanceSkiped = true;
                                                    }

                                                    break;
                                                case BrainWord.CheckInput.InputType.Released:
                                                    if ((Mind as Core).Inputs.Contains((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.CheckInput).Target) || !(Mind as Core).OldInputs.Contains((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.CheckInput).Target))
                                                    {
                                                        SentanceSkiped = true;
                                                    }

                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else if (Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.Action.Pass)
                                        {
                                            Part ToHoldFlags = null;
                                            if ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.Pass).Sanatize == true)
                                            {
                                                ToHoldFlags = FlagHolder;
                                            }
                                            Passed = true;
                                            List<DataLine> Target = [];
                                            foreach (Node item in GetParent().GetChildren())
                                            {
                                                if (item is Line2D)
                                                {
                                                    if (item.GetChildCount() > 0 && item.GetChild(0) is DataLine)
                                                    {
                                                        if ((item.GetChild(0) as DataLine).PassId == FindInt((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.Pass).PassId))
                                                        {
                                                            Target.Add(item.GetChild(0) as DataLine);
                                                        }
                                                    }
                                                }
                                            }
                                            List<Behaviour> ToSend = [new([[]])];

                                            while (WordId < Brain[CurrentBehaviour].Task[CurrentBrainSentence].Count - 1)
                                            {
                                                int SanatizeDepth = 0;
                                                WordId += 1;
                                                ToSend[0].Task[0].Add(Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId]);

                                                BrainWord Sanatize(BrainWord ToSanatize)
                                                {
                                                    SanatizeDepth += 1;
                                                    if (ToSanatize.SanatizePass == true)
                                                    {
                                                        if (ToSanatize is BrainWord.Flag)
                                                        {
                                                            if (ToSanatize is BrainWord.Flag.Value)
                                                            {
                                                                if (ToSanatize is BrainWord.Flag.Value.FlagReference)
                                                                {
                                                                    switch ((ToSanatize as BrainWord.Flag.Value.FlagReference).DestinationType)
                                                                    {
                                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                                            return new BrainWord.Flag.Value.Constant(BrainWord.Flag.FlagType.IntFlag, FindInt(ToSanatize as BrainWord.Flag.Value.FlagReference).ToString());
                                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                                            return new BrainWord.Flag.Value.Constant(BrainWord.Flag.FlagType.FloatFlag, FindFloat(ToSanatize as BrainWord.Flag.Value.FlagReference).ToString());
                                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                                            return new BrainWord.Flag.Value.Constant(BrainWord.Flag.FlagType.BoolFlag, FindBool(ToSanatize as BrainWord.Flag.Value.FlagReference).ToString());
                                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                                            return new BrainWord.Flag.Value.Constant(BrainWord.Flag.FlagType.StringFlag, FindString(ToSanatize as BrainWord.Flag.Value.FlagReference).ToString());
                                                                        default:
                                                                            return ToSanatize;
                                                                    }
                                                                }
                                                                if (ToSanatize is BrainWord.Flag.Value.RandomValue)
                                                                {
                                                                    return new BrainWord.Flag.Value.RandomValue((ToSanatize as BrainWord.Flag.Value.RandomValue).DestinationType, Sanatize((ToSanatize as BrainWord.Flag.Value.RandomValue).Minimum) as BrainWord.Flag.Value, Sanatize((ToSanatize as BrainWord.Flag.Value.RandomValue).Maximum) as BrainWord.Flag.Value);
                                                                }
                                                                else
                                                                {
                                                                    return ToSanatize;
                                                                }
                                                            }
                                                            else if (ToSanatize is BrainWord.Flag.Math)
                                                            {
                                                                return new BrainWord.Flag.Math((ToSanatize as BrainWord.Flag.Math).Destination, Sanatize((ToSanatize as BrainWord.Flag.Math).Input) as BrainWord.Flag.Value.Constant, (ToSanatize as BrainWord.Flag.Math).SelectedOperation);
                                                            }
                                                            else if (ToSanatize is BrainWord.Flag.ReadFlag)
                                                            {
                                                                return new BrainWord.Flag.ReadFlag((ToSanatize as BrainWord.Flag.ReadFlag).Destination, (ToSanatize as BrainWord.Flag.ReadFlag).SelectedOperation, Sanatize((ToSanatize as BrainWord.Flag.ReadFlag).Input) as BrainWord.Flag.Value.Constant);
                                                            }
                                                            else
                                                            {
                                                                return ToSanatize;
                                                            }
                                                        }
                                                        else if (ToSanatize is BrainWord.Action)
                                                        {
                                                            if (ToSanatize is BrainWord.Action.Pass)
                                                            {
                                                                return new BrainWord.Action.Pass(Sanatize((ToSanatize as BrainWord.Action.Pass).PassId) as BrainWord.Flag.Value, (ToSanatize as BrainWord.Action.Pass).Sanatize);
                                                            }
                                                            else if (ToSanatize is BrainWord.Action.PartSettings)
                                                            {
                                                                return new BrainWord.Action.PartSettings(Sanatize((ToSanatize as BrainWord.Action.PartSettings).Target) as BrainWord.Flag.Value.Constant, Sanatize((ToSanatize as BrainWord.Action.PartSettings).Input) as BrainWord.Flag.Value.Constant, (ToSanatize as BrainWord.Action.PartSettings).Operation);
                                                            }
                                                            else
                                                            {
                                                                return ToSanatize;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            return ToSanatize;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return ToSanatize;
                                                    }
                                                }
                                            }
                                            foreach (DataLine item in Target)
                                            {
                                                item.RunCommands(ToSend, ToHoldFlags);
                                            }
                                        }
                                        else if (Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.Action.Pull)
                                        {
                                            /*DataLine Target = null;
                                            foreach (Node item in GetParent().GetChildren())
                                            {
                                                if (item is Line2D)
                                                {
                                                    if (item.GetChildCount() > 0 && item.GetChild(0) is DataLine)
                                                    {
                                                        if ((item.GetChild(0) as DataLine).PullId == FindInt((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.Pull).PullId))
                                                        {
                                                            Target = item.GetChild(0) as DataLine;
                                                        }
                                                    }
                                                }
                                            }
                                            if (Target != null)
                                            {
                                                if ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.Pull).)
                                                {

                                                }
                                            }*/
                                        }
                                        else if (Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.Action.PartSettings)
                                        {
                                            switch (FindString((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Target))
                                            {
                                                case "Intake":
                                                    switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Operation)
                                                    {
                                                        case BrainWord.Flag.Math.Operation.Set:
                                                            Intake = FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Add:
                                                            if (Mind != null)
                                                            {
                                                                Intake += FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Subtract:
                                                            if (Mind != null)
                                                            {
                                                                Intake -= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.multiply:
                                                            if (Mind != null)
                                                            {
                                                                Intake *= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Divide:
                                                            if (Mind != null)
                                                            {
                                                                Intake /= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Modulus:
                                                            if (Mind != null)
                                                            {
                                                                Intake %= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    break;
                                                case "Output":
                                                    switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Operation)
                                                    {
                                                        case BrainWord.Flag.Math.Operation.Set:
                                                            Output = FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Add:
                                                            if (Mind != null)
                                                            {
                                                                Output += FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Subtract:
                                                            if (Mind != null)
                                                            {
                                                                Output -= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.multiply:
                                                            if (Mind != null)
                                                            {
                                                                Output *= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Divide:
                                                            if (Mind != null)
                                                            {
                                                                Output /= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.Math.Operation.Modulus:
                                                            if (Mind != null)
                                                            {
                                                                Output %= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    break;
                                                case "Speed":
                                                    if (this is Actuator)
                                                    {
                                                        switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Operation)
                                                        {
                                                            case BrainWord.Flag.Math.Operation.Set:
                                                                (this as Actuator).Speed = FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Add:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Actuator).Speed += FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Subtract:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Actuator).Speed -= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.multiply:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Actuator).Speed *= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Divide:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Actuator).Speed /= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Modulus:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Actuator).Speed %= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    else if (this is Servo)
                                                    {
                                                        switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Operation)
                                                        {
                                                            case BrainWord.Flag.Math.Operation.Set:
                                                                (this as Servo).Speed = FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Add:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Speed += FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Subtract:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Speed -= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.multiply:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Speed *= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Divide:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Speed /= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Modulus:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Speed %= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case "OutputOpen":
                                                    if (this is PowerLine)
                                                    {
                                                        switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Operation)
                                                        {
                                                            case BrainWord.Flag.Math.Operation.Set:
                                                                (this as PowerLine).OutputOpen = FindBool((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case "IntakeOpen":
                                                    if (this is PowerLine)
                                                    {
                                                        switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Operation)
                                                        {
                                                            case BrainWord.Flag.Math.Operation.Set:
                                                                (this as PowerLine).IntakeOpen = FindBool((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case "Angle":
                                                    if (this is Servo)
                                                    {
                                                        switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Operation)
                                                        {
                                                            case BrainWord.Flag.Math.Operation.Set:
                                                                (this as Servo).Angle = FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Add:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Angle += FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Subtract:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Angle -= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.multiply:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Angle *= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Divide:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Angle /= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            case BrainWord.Flag.Math.Operation.Modulus:
                                                                if (Mind != null)
                                                                {
                                                                    (this as Servo).Angle %= FindFloat((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.PartSettings).Input);
                                                                }
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else if (Mind != null && Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.Flag.Math)
                                        {
                                            BrainWord.Flag.Math ToDo = Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.Math;
                                            switch (ToDo.SelectedOperation)
                                            {
                                                case BrainWord.Flag.Math.Operation.Set:
                                                    switch (ToDo.Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (ShortTermIntFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    ShortTermIntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    ShortTermIntFlags.Add(ToDo.Destination.Destination, FindInt(ToDo.Input));
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (IntFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    IntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    IntFlags.Add(ToDo.Destination.Destination, FindInt(ToDo.Input));
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (ShortTermFloatFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    ShortTermFloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    ShortTermFloatFlags.Add(ToDo.Destination.Destination, FindFloat(ToDo.Input));
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (FloatFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    FloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    FloatFlags.Add(ToDo.Destination.Destination, FindFloat(ToDo.Input));
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (BoolFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    BoolFlags[ToDo.Destination.Destination] = FindBool(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    BoolFlags.Add(ToDo.Destination.Destination, FindBool(ToDo.Input));
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (BoolFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    BoolFlags[ToDo.Destination.Destination] = FindBool(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    BoolFlags.Add(ToDo.Destination.Destination, FindBool(ToDo.Input));
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (ShortTermIntFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    ShortTermStringFlags[ToDo.Destination.Destination] = FindString(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    StringFlags.Add(ToDo.Destination.Destination, FindString(ToDo.Input));
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (StringFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    StringFlags[ToDo.Destination.Destination] = FindString(ToDo.Input);
                                                                }
                                                                else
                                                                {
                                                                    StringFlags.Add(ToDo.Destination.Destination, FindString(ToDo.Input));
                                                                }
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.Math.Operation.Add:
                                                    switch (ToDo.Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermIntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) + FindInt(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                IntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) + FindInt(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermFloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) + FindFloat(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                FloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) + FindFloat(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermBoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) + FindInt(ToDo.Input) >= 1;
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                BoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) + FindInt(ToDo.Input) >= 1;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermStringFlags[ToDo.Destination.Destination] = FindString(ToDo.Destination) + FindString(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                StringFlags[ToDo.Destination.Destination] = FindString(ToDo.Destination) + FindString(ToDo.Input);
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.Math.Operation.Subtract:
                                                    switch (ToDo.Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermIntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) - FindInt(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                IntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) - FindInt(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermFloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) - FindFloat(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                FloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) - FindFloat(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermBoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) - FindInt(ToDo.Input) >= 1;
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                BoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) - FindInt(ToDo.Input) >= 1;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            string FinalString = FindString(ToDo.Destination);
                                                            if (FindString(ToDo.Destination).ToLowerInvariant().Contains(FindString(ToDo.Input)))
                                                            {
                                                                FinalString = FinalString.Replace(FindString(ToDo.Input), "");
                                                            }
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermStringFlags[ToDo.Destination.Destination] = FinalString;
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                StringFlags[ToDo.Destination.Destination] = FinalString;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.Math.Operation.multiply:
                                                    switch (ToDo.Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermIntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) * FindInt(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                IntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) * FindInt(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermFloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) * FindFloat(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                FloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) * FindFloat(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermBoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) * FindInt(ToDo.Input) >= 1;
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                BoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) * FindInt(ToDo.Input) >= 1;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.Math.Operation.Divide:
                                                    switch (ToDo.Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (FindInt(ToDo.Input) != 0)
                                                            {
                                                                if (ToDo.Destination.DestinationIsShortTerm == true)
                                                                {
                                                                    ShortTermIntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) / FindInt(ToDo.Input);
                                                                }
                                                                else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                                {
                                                                    IntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) / FindInt(ToDo.Input);
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermFloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) / FindFloat(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                FloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) / FindFloat(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (FindInt(ToDo.Input) != 0)
                                                            {
                                                                if (ToDo.Destination.DestinationIsShortTerm == true)
                                                                {
                                                                    ShortTermBoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) / FindInt(ToDo.Input) >= 1;
                                                                }
                                                                else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                                {
                                                                    BoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) / FindInt(ToDo.Input) >= 1;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.Math.Operation.Modulus:
                                                    switch (ToDo.Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (FindInt(ToDo.Input) != 0)
                                                            {
                                                                if (ToDo.Destination.DestinationIsShortTerm == true)
                                                                {
                                                                    ShortTermIntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) % FindInt(ToDo.Input);
                                                                }
                                                                else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                                {
                                                                    IntFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) % FindInt(ToDo.Input);
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                ShortTermFloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) % FindFloat(ToDo.Input);
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                FloatFlags[ToDo.Destination.Destination] = FindFloat(ToDo.Destination) % FindFloat(ToDo.Input);
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (FindInt(ToDo.Input) != 0)
                                                            {
                                                                if (ToDo.Destination.DestinationIsShortTerm == true)
                                                                {
                                                                    ShortTermBoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) % FindInt(ToDo.Input) >= 1;
                                                                }
                                                                else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                                {
                                                                    BoolFlags[ToDo.Destination.Destination] = FindInt(ToDo.Destination) % FindInt(ToDo.Input) >= 1;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            break;
                                                    }
                                                    break;
                                            }
                                        }
                                        else if (Mind != null && Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.Flag.ReadFlag)
                                        {
                                            BrainWord.Flag.ReadFlag ToDo = Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.ReadFlag;
                                            switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.ReadFlag).SelectedOperation)
                                            {
                                                case BrainWord.Flag.ReadFlag.Operation.FlagInintialized:
                                                    switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.ReadFlag).Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (!ShortTermIntFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                                break;
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (!IntFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (!ShortTermFloatFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (!FloatFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (!ShortTermBoolFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (!BoolFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (!ShortTermStringFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (!StringFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.ReadFlag.Operation.FlagUnInintialized:
                                                    switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.ReadFlag).Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (ShortTermIntFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                                break;
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (IntFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (ShortTermFloatFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (FloatFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (ShortTermBoolFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (BoolFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            if (ToDo.Destination.DestinationIsShortTerm == true)
                                                            {
                                                                if (ShortTermStringFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            else if (ToDo.Destination.DestinationIsShortTerm == false)
                                                            {
                                                                if (StringFlags.ContainsKey(ToDo.Destination.Destination))
                                                                {
                                                                    SentanceSkiped = true;
                                                                }
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.ReadFlag.Operation.FlagEquals:
                                                    switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.ReadFlag).Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (FindInt(ToDo.Destination) != FindInt(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (FindFloat(ToDo.Destination) != FindFloat(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (FindBool(ToDo.Destination) != FindBool(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            if (FindString(ToDo.Destination) != FindString(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.ReadFlag.Operation.FlagGreaterThan:
                                                    switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.ReadFlag).Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (FindInt(ToDo.Destination) <= FindInt(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (FindFloat(ToDo.Destination) <= FindFloat(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (FindInt(ToDo.Destination) <= FindInt(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            break;
                                                    }
                                                    break;
                                                case BrainWord.Flag.ReadFlag.Operation.FlagLessThan:
                                                    switch ((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Flag.ReadFlag).Destination.DestinationType)
                                                    {
                                                        case BrainWord.Flag.FlagType.IntFlag:
                                                            if (FindInt(ToDo.Destination) >= FindInt(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.FloatFlag:
                                                            if (FindFloat(ToDo.Destination) >= FindFloat(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.BoolFlag:
                                                            if (FindInt(ToDo.Destination) >= FindInt(ToDo.Input))
                                                            {
                                                                SentanceSkiped = true;
                                                            }
                                                            break;
                                                        case BrainWord.Flag.FlagType.StringFlag:
                                                            break;
                                                    }
                                                    break;
                                            }
                                        }
                                        else if (Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] is BrainWord.Action.Print)
                                        {
                                            GD.Print(GetType().ToString() + ": " + FindString((Brain[CurrentBehaviour].Task[CurrentBrainSentence][WordId] as BrainWord.Action.Print).ToPrint));
                                        }
                                        // foreach (var item in IntFlags)
                                        // {
                                        // 	GD.Print(item);
                                        // }
                                    }
                                    catch (System.Exception)
                                    {

                                        throw;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            long FindInt(BrainWord.Flag.Value Input)
            {
                long Result = 0;
                try
                {
                    if (Input is BrainWord.Flag.Value.Constant)
                    {
                        Result = int.Parse((Input as BrainWord.Flag.Value.Constant).Value);
                    }
                    else if (Input is BrainWord.Flag.Value.FlagReference)
                    {
                        switch ((Input as BrainWord.Flag.Value.FlagReference).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermIntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                else
                                {
                                    Result = FlagHolder.IntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = Mathf.RoundToInt(FlagHolder.ShortTermFloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                else
                                {
                                    Result = Mathf.RoundToInt(FlagHolder.FloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    if (FlagHolder.ShortTermBoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] == false)
                                    {
                                        Result = 0;
                                    }
                                    else
                                    {
                                        Result = 1;
                                    }
                                }
                                else
                                {
                                    if (FlagHolder.BoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] == false)
                                    {
                                        Result = 0;
                                    }
                                    else
                                    {
                                        Result = 1;
                                    }
                                }
                                break;
                            case BrainWord.Flag.FlagType.StringFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = int.Parse(FlagHolder.ShortTermStringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                else
                                {
                                    Result = int.Parse(FlagHolder.StringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                break;
                        }
                    }
                    else
                    {
                        GD.Randomize();
                        switch ((Input as BrainWord.Flag.Value.RandomValue).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                Result = World.TheWorld.Bob.RandiRange((int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Minimum), (int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Maximum));
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                Result = Mathf.RoundToInt(World.TheWorld.Bob.RandfRange(FindFloat((Input as BrainWord.Flag.Value.RandomValue).Minimum), FindFloat((Input as BrainWord.Flag.Value.RandomValue).Maximum)));
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                Result = World.TheWorld.Bob.RandiRange(0, 1);
                                break;
                        }
                    }
                }
                catch (System.Exception)
                {

                }

                return Result;
            }
            float FindFloat(BrainWord.Flag.Value Input)
            {
                float Result = 0;
                try
                {
                    if (Input is BrainWord.Flag.Value.Constant)
                    {
                        Result = float.Parse((Input as BrainWord.Flag.Value.Constant).Value);
                    }
                    else if (Input is BrainWord.Flag.Value.FlagReference)
                    {
                        switch ((Input as BrainWord.Flag.Value.FlagReference).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermIntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                else
                                {
                                    Result = FlagHolder.IntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermFloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                else
                                {
                                    Result = FlagHolder.FloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    if (FlagHolder.ShortTermBoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] == false)
                                    {
                                        Result = 0;
                                    }
                                    else
                                    {
                                        Result = 1;
                                    }
                                }
                                else
                                {
                                    if (FlagHolder.BoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] == false)
                                    {
                                        Result = 0;
                                    }
                                    else
                                    {
                                        Result = 1;
                                    }
                                }
                                break;
                            case BrainWord.Flag.FlagType.StringFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = float.Parse(FlagHolder.ShortTermStringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                else
                                {
                                    Result = float.Parse(FlagHolder.StringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                break;
                        }
                    }
                    else
                    {
                        GD.Randomize();
                        switch ((Input as BrainWord.Flag.Value.RandomValue).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                Result = World.TheWorld.Bob.RandiRange((int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Minimum), (int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Maximum));
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                Result = World.TheWorld.Bob.RandfRange(FindFloat((Input as BrainWord.Flag.Value.RandomValue).Minimum), FindFloat((Input as BrainWord.Flag.Value.RandomValue).Maximum));
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                Result = World.TheWorld.Bob.RandiRange(0, 1);
                                break;
                        }
                    }
                }
                catch (System.Exception)
                {

                }
                return Result;
            }
            bool FindBool(BrainWord.Flag.Value Input)
            {
                bool Result = false;
                try
                {
                    if (Input is BrainWord.Flag.Value.Constant)
                    {
                        Result = bool.Parse((Input as BrainWord.Flag.Value.Constant).Value);
                    }
                    else if (Input is BrainWord.Flag.Value.FlagReference)
                    {
                        switch ((Input as BrainWord.Flag.Value.FlagReference).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermIntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] >= 1;
                                }
                                else
                                {
                                    Result = FlagHolder.IntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] >= 1;
                                }
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermFloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] >= 1;
                                }
                                else
                                {
                                    Result = FlagHolder.FloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] >= 1;
                                }
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {

                                    Result = FlagHolder.ShortTermBoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                else
                                {
                                    Result = FlagHolder.BoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                break;
                            case BrainWord.Flag.FlagType.StringFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = bool.Parse(FlagHolder.ShortTermStringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                else
                                {
                                    Result = bool.Parse(FlagHolder.StringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination]);
                                }
                                break;
                        }
                    }
                    else
                    {
                        GD.Randomize();
                        switch ((Input as BrainWord.Flag.Value.RandomValue).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                Result = World.TheWorld.Bob.RandiRange((int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Minimum), (int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Maximum)) >= 1;
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                Result = Mathf.RoundToInt(World.TheWorld.Bob.RandfRange(FindFloat((Input as BrainWord.Flag.Value.RandomValue).Minimum), FindFloat((Input as BrainWord.Flag.Value.RandomValue).Maximum))) >= 1;
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                Result = World.TheWorld.Bob.RandiRange(0, 1) >= 1;
                                break;
                        }
                    }
                }
                catch (System.Exception)
                {


                }

                return Result;
            }
            string FindString(BrainWord.Flag.Value Input)
            {
                string Result = "";
                try
                {
                    if (Input is BrainWord.Flag.Value.Constant)
                    {
                        Result = (Input as BrainWord.Flag.Value.Constant).Value;
                    }
                    else if (Input is BrainWord.Flag.Value.FlagReference)
                    {
                        switch ((Input as BrainWord.Flag.Value.FlagReference).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermIntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination].ToString();
                                }
                                else
                                {
                                    Result = FlagHolder.IntFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination].ToString();
                                }
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermFloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination].ToString();
                                }
                                else
                                {
                                    Result = FlagHolder.FloatFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination].ToString();
                                }
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    if (FlagHolder.ShortTermBoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] == false)
                                    {
                                        Result = false.ToString();
                                    }
                                    else
                                    {
                                        Result = true.ToString();
                                    }
                                }
                                else
                                {
                                    if (FlagHolder.BoolFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination] == false)
                                    {
                                        Result = false.ToString();
                                    }
                                    else
                                    {
                                        Result = true.ToString();
                                    }
                                }
                                break;
                            case BrainWord.Flag.FlagType.StringFlag:
                                if ((Input as BrainWord.Flag.Value.FlagReference).DestinationIsShortTerm == true)
                                {
                                    Result = FlagHolder.ShortTermStringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                else
                                {
                                    Result = FlagHolder.StringFlags[(Input as BrainWord.Flag.Value.FlagReference).Destination];
                                }
                                break;
                        }
                    }
                    else
                    {
                        GD.Randomize();
                        switch ((Input as BrainWord.Flag.Value.RandomValue).DestinationType)
                        {
                            case BrainWord.Flag.FlagType.IntFlag:
                                Result = World.TheWorld.Bob.RandiRange((int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Minimum), (int)FindInt((Input as BrainWord.Flag.Value.RandomValue).Maximum)).ToString();
                                break;
                            case BrainWord.Flag.FlagType.FloatFlag:
                                Result = Mathf.RoundToInt(World.TheWorld.Bob.RandfRange(FindFloat((Input as BrainWord.Flag.Value.RandomValue).Minimum), FindFloat((Input as BrainWord.Flag.Value.RandomValue).Maximum))).ToString();
                                break;
                            case BrainWord.Flag.FlagType.BoolFlag:
                                bool asdf = World.TheWorld.Bob.RandiRange(0, 1) >= 1;
                                Result = asdf.ToString();
                                break;
                        }
                    }
                }
                catch (System.Exception)
                {

                }
                return Result;
            }
        }
        else
        {
            (this as DataLine).Target.RunCommands(Brain, FlagHolder);
        }
    }
}
