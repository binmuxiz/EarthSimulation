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
            Ending1,
            Ending2,
        }

        private void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
        }

        public async UniTask PrepareVideo(RawImage mScreen)
        {
            // 비디오 준비
            _videoPlayer.Prepare();

            while (!_videoPlayer.isPrepared)
            {
                await UniTask.WaitForSeconds(0.5f);
            }

            mScreen.texture = _videoPlayer.texture;
        }

        public void PlayVideo(VideoType type)
        {
            if (_videoPlayer != null && _videoPlayer.isPrepared)
            {
                var clip = mVideoClips[(int)type];
                _videoPlayer.clip = clip;
                _videoPlayer.Play();
            }
        }

        public void StopVideo()
        {
            if (_videoPlayer != null && _videoPlayer.isPrepared)
            {
                _videoPlayer.Stop();
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
