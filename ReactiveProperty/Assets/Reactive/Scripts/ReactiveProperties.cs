#nullable enable

using System;
using UnityEngine;

// ReSharper disable RedundantBaseConstructorCall

namespace Reactive
{
    [Serializable]
    public class BoolReactiveProperty : ReactiveProperty<bool>
    {
        public BoolReactiveProperty() : base()
        {
        }

        public BoolReactiveProperty(bool value) : base(value)
        {
        }
    }

    [Serializable]
    public class IntReactiveProperty : ReactiveProperty<int>
    {
        public IntReactiveProperty() : base()
        {
        }

        public IntReactiveProperty(int value) : base(value)
        {
        }
    }

    [Serializable]
    public class FloatReactiveProperty : ReactiveProperty<float>
    {
        public FloatReactiveProperty() : base()
        {
        }

        public FloatReactiveProperty(float value) : base(value)
        {
        }
    }

    [Serializable]
    public class LongReactiveProperty : ReactiveProperty<long>
    {
        public LongReactiveProperty() : base()
        {
        }

        public LongReactiveProperty(long value) : base(value)
        {
        }
    }

    [Serializable]
    public class ByteReactiveProperty : ReactiveProperty<byte>
    {
        public ByteReactiveProperty() : base()
        {
        }

        public ByteReactiveProperty(byte value) : base(value)
        {
        }
    }

    [Serializable]
    public class DoubleReactiveProperty : ReactiveProperty<double>
    {
        public DoubleReactiveProperty() : base()
        {
        }

        public DoubleReactiveProperty(double value) : base(value)
        {
        }
    }

    [Serializable]
    public class DecimalReactiveProperty : ReactiveProperty<decimal>
    {
        public DecimalReactiveProperty() : base()
        {
        }

        public DecimalReactiveProperty(decimal value) : base(value)
        {
        }
    }

    [Serializable]
    public class CharReactiveProperty : ReactiveProperty<char>
    {
        public CharReactiveProperty() : base()
        {
        }

        public CharReactiveProperty(char value) : base(value)
        {
        }
    }

    [Serializable]
    public class StringReactiveProperty : ReactiveProperty<string>
    {
        public StringReactiveProperty() : base()
        {
        }

        public StringReactiveProperty(string value) : base(value)
        {
        }
    }

    [Serializable]
    public class Vector2ReactiveProperty : ReactiveProperty<Vector2>
    {
        public Vector2ReactiveProperty() : base()
        {
        }

        public Vector2ReactiveProperty(Vector2 value) : base(value)
        {
        }
    }

    [Serializable]
    public class Vector3ReactiveProperty : ReactiveProperty<Vector3>
    {
        public Vector3ReactiveProperty() : base()
        {
        }

        public Vector3ReactiveProperty(Vector3 value) : base(value)
        {
        }
    }

    [Serializable]
    public class Vector4ReactiveProperty : ReactiveProperty<Vector4>
    {
        public Vector4ReactiveProperty() : base()
        {
        }

        public Vector4ReactiveProperty(Vector4 value) : base(value)
        {
        }
    }

    [Serializable]
    public class QuaternionReactiveProperty : ReactiveProperty<Quaternion>
    {
        public QuaternionReactiveProperty() : base()
        {
        }

        public QuaternionReactiveProperty(Quaternion value) : base(value)
        {
        }
    }

    [Serializable]
    public class ColorReactiveProperty : ReactiveProperty<Color>
    {
        public ColorReactiveProperty() : base()
        {
        }

        public ColorReactiveProperty(Color value) : base(value)
        {
        }
    }

    [Serializable]
    public class BoundsReactiveProperty : ReactiveProperty<Bounds>
    {
        public BoundsReactiveProperty() : base()
        {
        }

        public BoundsReactiveProperty(Bounds value) : base(value)
        {
        }
    }

    [Serializable]
    public class RectReactiveProperty : ReactiveProperty<Rect>
    {
        public RectReactiveProperty() : base()
        {
        }

        public RectReactiveProperty(Rect value) : base(value)
        {
        }
    }
}