namespace Meziantou.Framework.HumanReadable;

public abstract class HumanReadableConverterFactory : HumanReadableConverter
{
    public abstract HumanReadableConverter? CreateConverter(Type typeToConvert, HumanReadableSerializerOptions options);

    public sealed override void WriteValue(HumanReadableTextWriter writer, object? value, Type valueType, HumanReadableSerializerOptions options) => throw new InvalidOperationException();
}
