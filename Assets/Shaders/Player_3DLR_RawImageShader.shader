Shader "PlayerDemo/OES_3D_LR_RawImage"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        ZTest Always
        Pass
        {
            GLSLPROGRAM
            #include "UnityStereoSupport.glslinc"
            #include "UnityStereoExtensions.glslinc"

            #ifdef VERTEX

            in vec4 _glesVertex;
            in vec4 _glesMultiTexCoord0;
            out vec2 convertedCoord;

            void main()
            {
                int eyeIndex = SetupStereoEyeIndex();
                gl_Position = GetStereoMatrixVP(eyeIndex) * (unity_ObjectToWorld * _glesVertex);
                float x = (_glesMultiTexCoord0.x * 0.5) + (0.5 * float(eyeIndex));
                convertedCoord = vec2(x, (1.0 - _glesMultiTexCoord0.y));
            }

            #endif

            #ifdef FRAGMENT

            #extension GL_OES_EGL_image_external : require
            #extension GL_OES_EGL_image_external_essl3 : require

            in vec2 convertedCoord;
            uniform samplerExternalOES _MainTex;

            void main()
            {
                vec4 textureColor = texture2D(_MainTex, convertedCoord);
                gl_FragData[0] = vec4(pow(textureColor.rgb, vec3(2.2)), textureColor.a);
            }

            #endif

            ENDGLSL
        }
    }
    Fallback "Unlit/Texture"
}