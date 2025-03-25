# FPConnect - Página de Cambio de Contraseña

Esta es la página web para cambiar la contraseña de FPConnect.

## Requisitos

- Python 3.x instalado en tu sistema
- Los siguientes archivos en la misma carpeta:
  - `index.html`
  - `styles.css`
  - `script.js`
  - `server.py`
  - `logoFPConnect.png`

## Instrucciones de Uso

1. Abre una terminal (PowerShell o CMD)
2. Navega a la carpeta del proyecto:
   ```bash
   cd ruta/a/tu/carpeta
   ```
3. Ejecuta el servidor:
   ```bash
   python server.py
   ```
4. Abre tu navegador y visita:
   ```
   http://localhost:8000?token=e83c586b-ebcc-4bb2-93a1-59c6a16e6356
   ```

## Solución de Problemas

Si el servidor no inicia:
1. Verifica que Python está instalado:
   ```bash
   python --version
   ```
2. Verifica que estás en la carpeta correcta:
   ```bash
   dir
   ```
3. Asegúrate de que todos los archivos necesarios están presentes

## Notas

- El servidor se ejecuta en el puerto 8000
- Para detener el servidor, presiona Ctrl+C en la terminal
- Si el puerto 8000 está en uso, puedes cambiar el número de puerto en el archivo `server.py` 