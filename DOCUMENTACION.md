# Documentación del Juego 2D Lateral

## Descripción General
Juego 2D lateral donde controlas un personaje que puede moverse, saltar y disparar flechas para eliminar enemigos.

---

## Scripts

### 1. Persona.cs
Script del personaje jugable principal.

#### Variables
- **Movimiento:**
  - `velocidadMovimiento`: Velocidad de desplazamiento horizontal (default: 5)
  - `fuerzaSalto`: Fuerza aplicada al saltar (default: 10)
  
- **Disparo:**
  - `flechaPrefab`: Prefab de la flecha a disparar
  - `puntoDisparo`: Transform que indica dónde aparecen las flechas
  - `fuerzaFlecha`: Velocidad de la flecha (default: 15)
  - `tiempoEntreDisparos`: Tiempo de espera entre disparos (default: 0.5s)
  
- **Vida:**
  - `vidaMaxima`: Vida máxima del personaje (default: 100)
  - `vidaActual`: Vida actual

#### Métodos
- `Mover()`: Controla el movimiento horizontal y voltea el sprite según la dirección
- `Saltar()`: Aplica fuerza vertical para saltar (solo si está en el suelo)
- `Disparar()`: Instancia una flecha en la dirección que mira el personaje
- `Voltear()`: Invierte la escala X para cambiar la dirección visual
- `RecibirDanio(int cantidad)`: Resta vida al personaje
- `Morir()`: Se ejecuta cuando la vida llega a 0

#### Controles
- **A/D o Flechas Izq/Der**: Movimiento
- **Espacio**: Saltar
- **Click Izq o Ctrl**: Disparar

#### Requisitos
- Tag: "Player"
- Componente: Rigidbody2D (Body Type: Dynamic, Gravity Scale: 1)
- Componente: Collider2D (BoxCollider2D o CapsuleCollider2D)

---

### 2. Enemigo.cs
Script de enemigos que patrullan y atacan al jugador.

#### Variables
- **Movimiento:**
  - `velocidad`: Velocidad de patrulla (default: 2)
  - `distanciaPatrulla`: Distancia máxima que recorre antes de voltear (default: 5)
  
- **Combate:**
  - `danio`: Daño que hace por golpe (default: 10)
  - `tiempoEntreDanios`: Tiempo entre golpes consecutivos (default: 1s)

#### Métodos
- `Patrullar()`: Movimiento automático de ida y vuelta en un área
- `Voltear()`: Cambia de dirección cuando alcanza el límite
- `RecibirDanio(int cantidad)`: Destruye el enemigo (puede modificarse)
- `OnCollisionStay2D()`: Detecta contacto continuo con el jugador e inflige daño

#### Requisitos
- Tag: "Enemy"
- Componente: Rigidbody2D (Body Type: Dynamic, Gravity Scale: 1)
- Componente: Collider2D (BoxCollider2D o CapsuleCollider2D)

---

## Configuración en Unity

### Setup Inicial

1. **Crear Personaje:**
   - GameObject → Sprite → Square (o tu sprite)
   - Añade componente: Rigidbody2D
   - Añade componente: BoxCollider2D
   - Añade script: Persona.cs
   - Etiqueta: "Player"

2. **Crear Enemigo:**
   - GameObject → Sprite → Square (o tu sprite)
   - Añade componente: Rigidbody2D
   - Añade componente: BoxCollider2D
   - Añade script: Enemigo.cs
   - Etiqueta: "Enemy"

3. **Crear Flecha (Prefab):**
   - GameObject → Sprite → Square
   - Añade componente: Rigidbody2D
   - Añade componente: BoxCollider2D (marcar como trigger)
   - Crea una carpeta "Resources" en Assets
   - Arrastra la flecha a la carpeta y crea un Prefab

4. **Crear Suelo:**
   - GameObject → Sprite → Square
   - Escala: (10, 1, 1) aproximadamente
   - Añade componente: BoxCollider2D
   - Etiqueta: "Ground"

5. **Asignar Referencias:**
   - En el Inspector del Personaje:
     - Arrastra el prefab de flecha a "Flecha Prefab"
     - Crea un GameObject vacío como hijo del Personaje (posiciónalo al frente)
     - Arrastra ese GameObject al campo "Punto Disparo"

---

## Estados y Lógica

### Flujo de Juego
