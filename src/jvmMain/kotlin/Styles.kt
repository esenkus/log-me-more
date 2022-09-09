import androidx.compose.material.darkColors
import androidx.compose.ui.graphics.Color

val Color.Companion.NiceBackground
  get() = Color(0xFF073042)

val Color.Companion.NicePrimaryVariant
  get() = Color(0xFF1b9096)

val Color.Companion.NiceSecondary
  get() = Color(0xFF1c6a60)

val DarkTheme =
    darkColors(
        primary = Color.LightGray,
        primaryVariant = Color.NicePrimaryVariant,
        background = Color.NiceBackground,
        secondary = Color.NiceSecondary)
