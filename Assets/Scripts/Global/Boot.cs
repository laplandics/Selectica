using System.Collections;
using UnityEngine;

namespace Selectica
{
    public class Boot
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap() => _ = new Boot();

        private Boot()
        {
            G.Reset();
            R.Reload();
            
            Timer.Run();
            Debugger.Run();
            
            G.Register(new UI());
            G.Register(new Cells());
            G.Register(new Scenes());
            G.Register(new States());
            G.Register(new Inputs());
            G.Register(new Systems());
            G.Register(new Entities());
            G.Register(new Coroutines());
            
            G.Resolve<Scenes>().RegisterScene(new MenuScene());
            G.Resolve<Scenes>().RegisterScene(new GameScene());
            G.Resolve<States>().Activate();
            
            G.Resolve<Coroutines>().Start(MainBoot());
        }

        private IEnumerator MainBoot()
        {
            Main.IsPlaying = true;
            yield return null;
            
            G.Resolve<Inputs>().Activate();
            yield return null;
            
            yield return G.Resolve<Scenes>().LoadScene(Scenes.MENU);
            yield return null;
            
            Resources.UnloadUnusedAssets();
            yield return null;
        }
    }
}