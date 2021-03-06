# number-identification
Los archivos de esta prueba están dentro de `Data`, `Prefabs`, `Scenes` y `Scripts`. 
La carpeta TextMesh Pro ha sido necesario importarla para utilizar TMP en la UI del proyecto (no me la creó el proyecto por defecto pese a ser TMP un package por defecto de Unity).

Este proyecto consta de una única escena, `IdentificationScene`, localizada dentro de la carpeta `Assets/Scenes`. Está seteada para poder ejecutar una secuencia infinita de problemas de identificación de números por su literal nada más darle al play.
Breve descripción de la estructura:
- El control del flow de la escena se hace en la clase `ProblemController`. Desde ahí tenemos acceso tanto a los datos como las vistas con los distintos comportamientos.
- `ProblemController` tiene referencia a los archivos `ProblemsConfig` y `ScoreModel`, ambos scriptable objects. 
    - `ProblemsConfig` es donde definimos los "problemas" / "preguntas" que vamos a mostrar. Más info un poco más abajo
    - `ScoreModel` se resetea con cada ejecución, pero podría almacenar acumulados u otros datos / estadísticas interesantes.
- Dentro de UI hay tres vistas principales, `Question` (`QuestionView`), `AnswerContainer` (`AnswerViewContainer` y sus respectivos `AnswerViews`) y `Score`.
    - Todos ellos son prefabs para favorecer la edición y reutilización
    - Todas las animaciones son fade in / fade out por código. 
    - `AnswerContainer` además tiene un componente `HorizontalLayoutGroup` para soportar distintas cantidades de respuestas para los problemas. En el enunciado se pedían 3 pero soporta distintas cantidades en caso de que así se quiera


## ¿Cómo estan seteados los enunciados? => ProblemsConfig
Este scriptable object es donde se definen la tupla entre número en valor numérico y su representación en texto (`List<NumbersConfig>`). Internamente se le pide a la clase que devuelva un `ProblemConfig` con su enunciado y sus respuestas, marcando cual es la correcta, para que `ProblemsController` pueda setear la vista correctamente y seguir con el flow.
En el código proporcionado se establecen las relaciones desde 0 hasta 9, pero admitiría tantas como se gusten.

También tiene un parámetro `ResponsesAvailable` con el que es posible determinar cuantas respuestas en total se plantean por problema. Se entrega seteado a 3, pero se puede cambiar. Internamente, se mirará cuantas respuestas en total se pueden dar sin repetirse (si solo hay 10 valores en `Numbers` no tiene sentido dar más respuestas pese a que aquí configuremos un número mayor)

¡Sin embargo! Antes de esta implementación había entendido ligeramente distinto el enunciado, y en lugar de hacer que se generaran de forma random los enunciados había entendido que se generaban íntegramente a mano. Y ahí es donde entra la sección `Custom Problems`.  
Esta sección tiene un flag `Use Custom Problems` donde podemos especificar que en lugar de usar problemas autogenerados se usen los de la lista que hay en el archivo. Se entrega uno en el código a modo de ejemplo.

Es importante resaltar que si usamos los problemas custom sólo saldrán tantos problemas como los configurados en la lista y no serán infinitos. Probablemente la estructura de ProblemsConfig habría sido ligeramente distinta sin estos problemas "ad hoc" pero me parecía una pena eliminarlos y realmente dan mucha libertad para probar toda la escena.

## Veo aciertos y fallos pero, ¿por qué hay un contador de segundo intento?
He decidido incluir ese contador porque creo que es relevante saber la proporción de aciertos a la primera frente a aciertos una vez se ha eliminado una de las respuestas erróneas. No afecta al recuento de aciertos vs fallos.

