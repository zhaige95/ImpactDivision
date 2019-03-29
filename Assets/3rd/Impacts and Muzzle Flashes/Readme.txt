version 1.0.0.0

NOTE: Demo worked with bloom and HDR. For corrected bloom with "Forward" mode disable multisampling, or use "Deffered" mode.  "Edit->Project Settings->Quality->Multisampling" 

FPS pack works on mobile / PC / consoles with vertexlit / forward / deferred renderer and dx9, dx11, openGL. Unity4 and Unity5 supported.
All effects optimized for mobile and pc. So you can use this effects even on old mobiles. For mobile uses optimized shaders.
 
For scaleyou need add the script "FPSParticleSystemScaler" on prefab effect and change "scale" property. 
Also, on Unity 5.3 you can change particle system by tranform scale. 

For creating effect in runtime, just use follow code:

var instanceEffect = Instantiate(Effect, Position, new Quaternion()) as GameObject;

You can use "objects pool" for effects optimizing. Just reactivate effect after time. 

instanceEffect.SetActive(false);
instanceEffect.SetActive(true)

If you have some questions, you can write me to email "kripto289@gmail.com" 