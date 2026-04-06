using System.Collections.Generic;

public static class CSSClasses
{
    #region Classes by UIScale
    public static readonly Dictionary<UIScale, string> ScrapIcon = new()

    {
        { UIScale.SMALL, "icon-small" },
        { UIScale.MEDIUM, "icon-medium" },
        { UIScale.LARGE, "icon-large" },
        { UIScale.XL, "icon-xl" }
    };

    public static readonly Dictionary<UIScale, string> FontSizes = new()
    {
        { UIScale.SMALL, "font-size-small" },
        { UIScale.MEDIUM, "font-size-medium" },
        { UIScale.LARGE, "font-size-large" },
        { UIScale.XL, "font-size-xl" }
    };

    public static readonly Dictionary<UIScale, string> BatterySizes = new()
    {
        { UIScale.SMALL, "battery-small" },
        { UIScale.MEDIUM, "battery-medium" },
        { UIScale.LARGE, "battery-large" },
        { UIScale.XL, "battery-xl" }
    };

    public static readonly Dictionary<UIScale, string> HeartSizes = new()
    {
        { UIScale.SMALL, "heart-small" },
        { UIScale.MEDIUM, "heart-medium" },
        { UIScale.LARGE, "heart-large" },
        { UIScale.XL, "heart-xl" }
    };
    #endregion
    #region Classes by Key
    public static readonly Dictionary<ScrapUIKeys, string> ScrapUI = new()
    {
        { ScrapUIKeys.PLUS_DEFAULT, "plus-scrap-default" },
        { ScrapUIKeys.MINUS_DEFAULT, "minus-scrap-default" },
        { ScrapUIKeys.UP_ANIMATION, "label-scrap-up" }
    };

    public static readonly Dictionary<BuildUIKeys, string> BuildUI = new()
    {
        { BuildUIKeys.TOP_ENABLED, "build-ui-up-active" },
        { BuildUIKeys.BOTTOM_ENABLED, "build-ui-down-active" },
        { BuildUIKeys.TOP_DISABLED, "build-ui-up-inactive" },
        { BuildUIKeys.BOTTOM_DISABLED, "build-ui-down-inactive" }
    };
    #endregion

    public enum ScrapUIKeys
    {
        PLUS_DEFAULT,
        MINUS_DEFAULT,
        UP_ANIMATION
    }

    public enum BuildUIKeys
    {
        TOP_ENABLED,
        BOTTOM_ENABLED,
        TOP_DISABLED,
        BOTTOM_DISABLED
    }

    public static string GetByScale(Dictionary<UIScale, string> dictionary, UIScale scale)
    {
        if (dictionary.ContainsKey(scale))
        {
            return dictionary[scale];
        }

        // fallback
        return dictionary[UIScale.SMALL];
    }

}