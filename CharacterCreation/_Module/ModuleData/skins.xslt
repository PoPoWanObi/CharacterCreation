<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output omit-xml-declaration="yes" />
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="woman"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_2_male"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <voice_type name="male_07" />
            <voice_type name="male_08" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_2_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_2_female"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <voice_type name="female_05" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_1_male"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <voice_type name="male_02" />
            <xsl:copy-of select="*" />
            <voice_type name="male_04" />
            <voice_type name="male_05" />
            <voice_type name="male_06" />
            <voice_type name="male_07" />
            <voice_type name="male_08" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_1_male"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture8,face_texture9,face_texture10" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_1_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_1_female"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <voice_type name="female_01" />
            <voice_type name="female_02" />
            <xsl:copy-of select="*" />
            <voice_type name="female_04" />
            <voice_type name="female_05" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_3_male"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <voice_type name="male_02" />
            <xsl:copy-of select="*" />
            <voice_type name="male_04" />
            <voice_type name="male_05" />
            <voice_type name="male_06" />
            <voice_type name="male_07" />
            <voice_type name="male_08" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_3_male"]/eyebrow_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <eyebrow_mesh name="male_eyebrow_2" />
            <eyebrow_mesh name="male_eyebrow_3" />
            <eyebrow_mesh name="male_eyebrow_4" />
            <eyebrow_mesh name="male_eyebrow_5" />
            <eyebrow_mesh name="male_eyebrow_7" />
            <eyebrow_mesh name="male_eyebrow_8" />
            <eyebrow_mesh name="male_eyebrow_9" />
            <eyebrow_mesh name="male_eyebrow_10" />
            <eyebrow_mesh name="male_eyebrow_6" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_3_female"]/eyebrow_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <eyebrow_mesh name="female_eyebrow_2" />
            <eyebrow_mesh name="female_eyebrow_3" />
            <eyebrow_mesh name="female_eyebrow_4" />
            <xsl:copy-of select="*" />
            <eyebrow_mesh name="female_eyebrow_7" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_3_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <face_texture name="head_female_a" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_b" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture3" />
            <face_texture name="head_female_c" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture4" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="kid_3_female"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <voice_type name="female_01" />
            <voice_type name="female_02" />
            <xsl:copy-of select="*" />
            <voice_type name="female_04" />
            <voice_type name="female_05" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="toddler_male"]/eyebrow_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <eyebrow_mesh name="male_eyebrow_2" />
            <eyebrow_mesh name="male_eyebrow_3" />
            <eyebrow_mesh name="male_eyebrow_4" />
            <eyebrow_mesh name="male_eyebrow_5" />
            <eyebrow_mesh name="male_eyebrow_7" />
            <eyebrow_mesh name="male_eyebrow_8" />
            <eyebrow_mesh name="male_eyebrow_9" />
            <eyebrow_mesh name="male_eyebrow_10" />
            <eyebrow_mesh name="male_eyebrow_6" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="toddler_male"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture1,face_texture2" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture3,face_texture4" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture5,face_texture6,face_texture7" />
            <xsl:copy-of select="*" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="toddler_male"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <voice_type name="male_02" />
            <xsl:copy-of select="*" />
            <voice_type name="male_04" />
            <voice_type name="male_05" />
            <voice_type name="male_06" />
            <voice_type name="male_07" />
            <voice_type name="male_08" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="toddler_female"]/eyebrow_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <eyebrow_mesh name="female_eyebrow_2" />
            <eyebrow_mesh name="female_eyebrow_3" />
            <eyebrow_mesh name="female_eyebrow_4" />
            <xsl:copy-of select="*" />
            <eyebrow_mesh name="female_eyebrow_7" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="toddler_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <face_texture name="head_female_a" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
            <face_texture name="head_female_e" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
            <face_texture name="head_female_b" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture3" />
            <face_texture name="head_female_c" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture4" />
            <xsl:copy-of select="*" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skins/race[@id="human"]/skin[@name="toddler_female"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <voice_type name="female_02" />
            <voice_type name="female_03" />
            <voice_type name="female_04" />
            <voice_type name="female_05" />
        </xsl:copy>
    </xsl:template>
</xsl:stylesheet>