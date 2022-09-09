import androidx.compose.desktop.ui.tooling.preview.Preview
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Window
import androidx.compose.ui.window.application

@Composable
fun myAppTheme(darkTheme: Boolean = true, content: @Composable () -> Unit) {
  val colors = if (darkTheme) DarkTheme else lightColors()

  MaterialTheme(
      colors = colors,
      content = {
        ProvideTextStyle(value = TextStyle(color = MaterialTheme.colors.primary), content = content)
      })
}

@Composable
@Preview
fun app() {
  var selectedDevice: String?
  var selectedProcess: String?

  val logStates = LogLevels.values().map { it.value }.associateWith { true }.toMutableMap()
  val logKeyStates = logKeys.associateWith { true }.toMutableMap()

  var keyFilterText by remember {
    mutableStateOf("Key Filter (${logKeyStates.count { it.value }})")
  }

  var keySearch by remember { mutableStateOf("") }
  var valueSearch by remember { mutableStateOf("") }

  myAppTheme {
    Column(
        modifier =
            Modifier.background(MaterialTheme.colors.background).fillMaxSize().padding(20.dp)) {
          Row(
              modifier = Modifier.padding(bottom = 40.dp).fillMaxWidth(),
              Arrangement.spacedBy(10.dp)) {
                dropdownList("Device Picker", devices) { selectedDevice = it }
                dropdownList("Process Picker", processes) { selectedProcess = it }
              }
          Row(modifier = Modifier.padding(bottom = 20.dp).fillMaxWidth()) {
            dropdownCheckboxList("LOG Level Filter", logStates) { key, value ->
              println("Changed state of $key to $value")
              logStates[key] = value
            }
            Box(modifier = Modifier.width(50.dp).weight(1f)) {}
            dropdownCheckboxList(keyFilterText, logKeyStates) { key, value ->
              println("Changed state of $key to $value")
              logKeyStates[key] = value
              keyFilterText = "Key Filter (${logKeyStates.count { it.value }})"
            }
          }
          Row(modifier = Modifier.padding(bottom = 20.dp).fillMaxWidth()) {
            Text(text = "Key Search", modifier = Modifier.weight(1f))
            Text(text = "Value Search", modifier = Modifier.weight(1f), textAlign = TextAlign.End)
          }
          Row(modifier = Modifier.padding(bottom = 20.dp).fillMaxWidth()) {
            TextField(
                singleLine = true,
                value = keySearch,
                modifier = Modifier.weight(1f).padding(end = 10.dp),
                onValueChange = { keySearch = it })
            TextField(
                singleLine = true,
                value = valueSearch,
                modifier = Modifier.weight(1f),
                onValueChange = { valueSearch = it })
          }

          TextField(
              value = loremIpsumText,
              modifier = Modifier.fillMaxSize(),
              onValueChange = { println("value changed: $it") })
        }
  }
}

fun main() = application {
  Window(onCloseRequest = ::exitApplication, title = "Log Me More") { app() }
}
