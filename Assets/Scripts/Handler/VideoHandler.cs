using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Handler
{
    public class VideoHandler: MonoBehaviour
    {
        public VideoClip[] mVideoClips;
        
        private VideoPlayer _videoPlayer;

        public enum VideoType
        {
            Loading,
            Connecting,
            Ending1,
            Ending2,
            Ending3
        }

        private void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            DontDestroyOnLoad(this);
        }

        public async UniTask PrepareVideo(RawImage mScreen, VideoType videoType)
        {
            Debug.Log("PrepareVideos()");

            // 비디오 클립이 설정되었는지 확인
            var clip = mVideoClips[(int)videoType];
            if (clip == null)
            {
                Debug.LogError("비디오 클립이 설정되지 않았습니다.");
                return;
            }
            
            // 비디오 플레이어에 클립 할당
            _videoPlayer.clip = clip;
            
            // 비디오 준비
            _videoPlayer.Prepare();
            
            while (!_videoPlayer.isPrepared)
            {
                Debug.Log("preparing...");
                await UniTask.WaitForSeconds(0.5f);
            }

            
            Debug.Log($"mScreen.texture: {mScreen.texture}");
        }

        public void PlayVideo()
        {
            Debug.Log("PlayVideo()");

            if (_videoPlayer != null && _videoPlayer.isPrepared)
            {
                _videoPlayer.Play();
                Debug.Log("비디오 Play()");
            }
            else
            {
                Debug.LogError("비디오가 준비되지 않았습니다.");
            }
        }


        public void SetVideoClip(VideoClip clip)
        {
            _videoPlayer.clip = clip;
        }

        public void StopVideo()
        {
            if (_videoPlayer != null && _videoPlayer.isPrepared)
            {
                _videoPlayer.Stop();
                _videoPlayer.clip = null;
            }
        }

        public void PauseVideo()
        {
            if (_videoPlayer != null && _videoPlayer.isPrepared)
            {
                _videoPlayer.Pause();
            }
        }
    }
}
