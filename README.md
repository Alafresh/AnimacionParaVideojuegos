# AnimacionParaVideojuegos
# Entrega 2: TPS Controller, Lock-On suavizado y Recoil con curva

## Resumen

Entrega basada en el MVP del controlador TPS con 3 personajes jugables y dos profundizaciones elegidas:

1. Lock-On suavizado con ajuste de cámara y marcador 3D a 2D con clamp y color por distancia.
2. Modulación de recoil con curva de recuperación para apuntado vs disparo hip.

---

## Contenidos del repo

* Rama: Entrega2
* Carpeta: Assets/LockOnSuavizado

---

## Controles

* Movimiento: WASD
* Mirar cámara: mouse
* Apuntar: clic derecho
* Disparar: clic izquierdo
* Lock-On alternar: tecla del centro del ratón

---

## Cómo correrlo

1. Clonar la rama Entrega2.
2. Abrir el proyecto en Unity.
3. Cargar la escena ubicada en Assets/LockOnSuavizado.
4. Pulsar Play.

---

## Personajes jugables

Se incluyen 3 personajes con diferencias claras entre varios parámetros como velocidad, fire rate, fuerza de recoil, zoom al apuntar y animación de recoil.

---

## Profundizaciones implementadas

### 1) Lock-On suavizado: cámara y marcador 3D a 2D con clamp y color por distancia

*Qué hace*

* Al activar Lock-On, la cámara desplaza ligeramente el encuadre para posicionar al personaje en un lateral tipo over-the-shoulder, mejorando la composición del objetivo en pantalla.
* Se dibuja un marcador 2D que sigue al objetivo proyectando su posición 3D a la pantalla.
* El marcador se clampa a los bordes si el objetivo sale del encuadre y cambia de color según la distancia para brindar feedback visual inmediato. Esta lógica de clamping, proyección y color está en WaypointIndicator.cs, que calcula la posición con WorldToScreenPoint, detecta si el objetivo queda detrás del jugador, clampa X e Y a la pantalla y pinta verde, amarillo o rojo según el rango de metros.
* Esta profundización corresponde a lo solicitado en el documento de requisitos.

*Cómo probarlo*

1. Moverse y rotar cámara para forzar que el objetivo entre y salga de cuadro.
2. Verificar que el marcador se mantenga visible en borde cuando el objetivo queda fuera de encuadre y que cambie de color con la distancia.
3. Activar Lock-On y comprobar que la cámara encuadre al objectivo.
4. Desactivar Lock-On y comprobar que la cámara vuelve a la composición estándar.

*Posibles edge cases*

* Objetivo detrás del jugador o fuera del FOV. El script fuerza el marcador al borde adecuado al detectar el dot product negativo y empuja X a min o max.
* Cambios de resolución o escalado de UI. Se usa GetPixelAdjustedRect para calcular half-width y half-height del ícono, lo cual ayuda a clamping consistente.

---

### 2) Modulación de recoil + curva de recuperación

*Qué hace*

* Diferencia el retroceso entre disparo sin apuntar y apuntando.
* Aplica una curva de recuperación para controlar cómo sube y baja el shake, no únicamente de forma lineal.
* Se integra con el ruido de cámara de Cinemachine para el “shake” y con una AnimationCurve para el retorno temporal, de modo que puedas perfilar respuesta suave, agresiva o escalonada según tu diseño. Esta profundización está listada en el documento de requisitos.

*Cómo probarlo*

1. Disparar sin apuntar y observar amplitud de recoil.
2. Apuntar y disparar, comparando la respuesta más contenida.
3. Disparar ráfagas y soltar el gatillo para observar la recuperación gobernada por la curva.
4. Editar la AnimationCurve en el inspector para ver cambios en la velocidad de asentamiento.

---

## Parámetros ajustables recomendados

* Lock-On

  * Offset de cámara para composición over-the-shoulder
  * Radio y ángulo de adquisición
  * Velocidad de suavizado al alternar Lock-On
  * Umbrales de distancia para colores del marcador
* Recoil

  * Amplitud hip-fire y amplitud ADS
  * Curva de recuperación
  * Velocidad de retorno y mezcla con otros ruidos de cámara

---

## Notas de implementación

* WaypointIndicator.cs usa WorldToScreenPoint, Mathf.Clamp y distancia en metros para actualizar posición, color y etiqueta del marcador cada frame. Considera el caso “objetivo detrás” con un dot product y ajusta la X al borde apropiado.
* CharacterGun.cs centraliza la lógica de disparo y conecta con el recoil de cámara por medio del componente RecoilCameraKick. La curva de recuperación se edita desde el inspector para perfilar la sensación final.
