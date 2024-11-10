# Online-store

 Unity version 2021.3.30

                         # To run the application

- ** option one - You need to go to the folder Build Files and run start_server.bat (Python must be installed on the computer).

- ** option to -  Launch Unity and create your Build using Build and Run
     to download the application you need to download the folder Online store - Mocart and open it as a project in Unity.

 - ** option three -  Run a local server from any other application in the build folder (For example : "Online Shop folder").

### Code Structure and Design

- **Scripts Folder**: Contains the main scripts of the project, including `DataFetcher` for loading data from the server, `ProductUIManager` for managing product data editing.
- **DataFetcher**: Class responsible for receiving JSON data from the server and dynamically creating objects on the scene.
- **ProductUIManager**: Manages the product data editing interface, including displaying and saving changes.


### Libraries and assets used

- **System.Collections / System.Collections.Generic**: Used to work with collections (e.g. `List<Product>`) in the project.
- **UnityEngine**: The core Unity namespace, providing access to components, scenes, and objects in the scene.
- **UnityEngine.Networking**: Used to interact with the server and load data via `UnityWebRequest`.
- **System**: Hooks up the core C# classes, including basic types and date operations.
- **System.Linq**: Provides LINQ for easy manipulation of collections, which helps with filtering and processing data.
- **TMPro (TextMeshPro)**: Used to render high-quality text in the UI, including `TMP_InputField` for editing text.
- **System.Runtime.InteropServices**: Enables support for calling platform-specific APIs and for interacting with JavaScript in WebGL.
- **All assets**: Brief description of usage, where it is taken from "free3d" -  https://free3d.com/. 