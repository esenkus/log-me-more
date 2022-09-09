import androidx.compose.foundation.background
import androidx.compose.foundation.border
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp

@Composable
fun dropdownList(baseText: String, items: List<String>, onSelection: (String) -> Unit) {
  var selectedIndex by remember { mutableStateOf(-1) }
  var expanded by remember { mutableStateOf(false) }
  Box(Modifier.border(width = 1.dp, color = MaterialTheme.colors.primaryVariant)) {
    Text(
        if (selectedIndex < 0) baseText else items[selectedIndex],
        Modifier.padding(5.dp).defaultMinSize(200.dp).clickable(onClick = { expanded = true }))
    DropdownMenu(
        expanded = expanded,
        onDismissRequest = { expanded = false },
        modifier = Modifier.background(MaterialTheme.colors.background)) {
          items.forEachIndexed { index, item ->
            DropdownMenuItem(
                onClick = {
                  selectedIndex = index
                  expanded = false
                  onSelection(item)
                },
                enabled = selectedIndex != index,
            ) {
              Text(item)
            }
          }
        }
  }
}

@Composable
fun dropdownCheckboxList(
    baseText: String,
    items: Map<String, Boolean>,
    onChange: (String, Boolean) -> Unit
) {
  var expanded by remember { mutableStateOf(false) }
  val values = remember { mutableStateMapOf<String, Boolean>() }
  values.putAll(items)

  fun updateValues(key: String, value: Boolean) {
    values[key] = value
    onChange(key, value)
  }

  fun updateAllValues(value: Boolean) {
    values.keys.forEach { updateValues(it, value) }
  }

  Box(Modifier.border(width = 1.dp, color = MaterialTheme.colors.primaryVariant)) {
    Text(
        baseText,
        Modifier.padding(5.dp).defaultMinSize(200.dp).clickable(onClick = { expanded = true }))
    DropdownMenu(
        expanded = expanded,
        onDismissRequest = { expanded = false },
        modifier = Modifier.background(MaterialTheme.colors.background)) {
          Row(
              modifier = Modifier.padding(10.dp),
              horizontalArrangement = Arrangement.spacedBy(10.dp)) {
                Button(
                    onClick = { updateAllValues(true) },
                    colors =
                        ButtonDefaults.buttonColors(
                            backgroundColor = MaterialTheme.colors.secondary)) {
                      Text("Select All")
                    }
                Button(
                    onClick = { updateAllValues(false) },
                    colors =
                        ButtonDefaults.buttonColors(
                            backgroundColor = MaterialTheme.colors.secondary)) {
                      Text("Select None")
                    }
              }
          values.forEach { (key, value) ->
            DropdownMenuItem(
                onClick = { updateValues(key, !value) },
            ) {
              Row {
                Checkbox(checked = values[key]!!, onCheckedChange = { updateValues(key, !value) })
                Text(key, modifier = Modifier.align(Alignment.CenterVertically))
              }
            }
          }
        }
  }
}
