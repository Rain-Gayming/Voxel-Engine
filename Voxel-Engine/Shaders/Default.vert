layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aNormal;

out vec2 texCoord;
out vec3 normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    texCoord = aTexCoord;
    normal = aNormal;

    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
    normal = aNormal * mat3(transpose(inverse(model)));
}