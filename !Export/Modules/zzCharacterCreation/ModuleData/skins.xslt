<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output omit-xml-declaration="yes" />
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="woman"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_male"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <voice_type name="male_07" />
            <voice_type name="male_08" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_male"]/hair_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <hair_mesh name="hair_male_h_d" cover_type1="hair_male_h_d_a" cover_type2="hair_male_h_d_b" cover_type4="hair_male_h_d">
                <style_tags>
                    <style_tag name="Afro Hair 5" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="hair_male_h_e" cover_type1="hair_male_h_e_a" cover_type4="hair_male_h_e">
                <style_tags>
                    <style_tag name="Viking Hair" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="hair_male_h_f" cover_type4="hair_male_h_f">
                <style_tags>
                    <style_tag name="Monk Hair" />
                </style_tags>
            </hair_mesh>
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_male"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="85">
            <tattoo_material name="tattoo_male_a_mat" tags="capon">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_male_b_mat" tags="capon" />
            <tattoo_material name="tattoo_male_c_mat" tags="capon" />
            <tattoo_material name="tattoo_male_d_mat" tags="capon" />
            <tattoo_material name="tattoo_male_e_mat" tags="capon" />
            <tattoo_material name="tattoo_male_f_mat" tags="capon" />
            <tattoo_material name="tattoo_male_g_mat" tags="capon" />
            <tattoo_material name="tattoo_male_h_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_i_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_j_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_k_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_l_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_m_mat" tags="capon,turk" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
            <tattoo_material name="scar_male_q" tags="capon" />
            <tattoo_material name="scar_male_o" tags="scar" />
            <tattoo_material name="scar_male_p" tags="scar" />
            <tattoo_material name="scar_male_r" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_female"]/hair_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <hair_mesh name="female_hair_z_h" cover_type1="female_hair_z_h_a" cover_type4="female_hair_z_h">
                <style_tags>
                    <style_tag name="High Ponytail" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="female_hair_z_i" cover_type1="female_hair_z_i_a" cover_type4="female_hair_z_i">
                <style_tags>
                    <style_tag name="High Ponytail" />
                </style_tags>
            </hair_mesh>
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_female"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="85">
            <tattoo_material name="tattoo_female_a_mat" tags="tattoo1">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_female_b_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_c_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_d_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_e_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_f_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_g_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_h_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_i_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_j_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_k_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_l_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_m_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_n_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_o_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_p_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_q_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_r_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_s_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_t_mat" tags="tattoo1" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_2_female"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <voice_type name="female_05" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_male"]/voice_types'>
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

    <xsl:template match='skin[@name="kid_1_male"]/hair_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <hair_mesh name="hair_male_h_d" cover_type1="hair_male_h_d_a" cover_type2="hair_male_h_d_b" cover_type4="hair_male_h_d">
                <style_tags>
                    <style_tag name="Afro Hair 5" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="hair_male_h_e" cover_type1="hair_male_h_e_a" cover_type4="hair_male_h_e">
                <style_tags>
                    <style_tag name="Viking Hair" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="hair_male_h_f" cover_type4="hair_male_h_f">
                <style_tags>
                    <style_tag name="Monk Hair" />
                </style_tags>
            </hair_mesh>
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_male"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="100">
            <tattoo_material name="tattoo_male_a_mat" tags="capon">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_male_b_mat" tags="capon" />
            <tattoo_material name="tattoo_male_c_mat" tags="capon" />
            <tattoo_material name="tattoo_male_d_mat" tags="capon" />
            <tattoo_material name="tattoo_male_e_mat" tags="capon" />
            <tattoo_material name="tattoo_male_f_mat" tags="capon" />
            <tattoo_material name="tattoo_male_g_mat" tags="capon" />
            <tattoo_material name="tattoo_male_h_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_i_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_j_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_k_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_l_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_m_mat" tags="capon,turk" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
            <tattoo_material name="scar_male_q" tags="capon" />
            <tattoo_material name="scar_male_o" tags="scar" />
            <tattoo_material name="scar_male_p" tags="scar" />
            <tattoo_material name="scar_male_r" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_male"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture8,face_texture9,face_texture10" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_female"]/hair_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <hair_mesh name="female_hair_z_h" cover_type1="female_hair_z_h_a" cover_type4="female_hair_z_h">
                <style_tags>
                    <style_tag name="High Ponytail" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="female_hair_z_i" cover_type1="female_hair_z_i_a" cover_type4="female_hair_z_i">
                <style_tags>
                    <style_tag name="High Ponytail" />
                </style_tags>
            </hair_mesh>
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_female"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="100">
            <tattoo_material name="tattoo_female_a_mat" tags="tattoo1">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_female_b_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_c_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_d_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_e_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_f_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_g_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_h_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_i_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_j_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_k_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_l_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_m_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_n_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_o_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_p_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_q_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_r_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_s_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_t_mat" tags="tattoo1" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_1_female"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <voice_type name="female_01" />
            <voice_type name="female_02" />
            <xsl:copy-of select="*" />
            <voice_type name="female_04" />
            <voice_type name="female_05" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_male"]/voice_types'>
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

    <xsl:template match='skin[@name="kid_3_male"]/hair_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <hair_mesh name="hair_male_h_d" cover_type1="hair_male_h_d_a" cover_type2="hair_male_h_d_b" cover_type4="hair_male_h_d">
                <style_tags>
                    <style_tag name="Afro Hair 5" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="hair_male_h_e" cover_type1="hair_male_h_e_a" cover_type4="hair_male_h_e">
                <style_tags>
                    <style_tag name="Viking Hair" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="hair_male_h_f" cover_type4="hair_male_h_f">
                <style_tags>
                    <style_tag name="Monk Hair" />
                </style_tags>
            </hair_mesh>
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_male"]/eyebrow_meshes'>
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

    <xsl:template match='skin[@name="kid_3_male"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="100">
            <tattoo_material name="tattoo_male_a_mat" tags="capon">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_male_b_mat" tags="capon" />
            <tattoo_material name="tattoo_male_c_mat" tags="capon" />
            <tattoo_material name="tattoo_male_d_mat" tags="capon" />
            <tattoo_material name="tattoo_male_e_mat" tags="capon" />
            <tattoo_material name="tattoo_male_f_mat" tags="capon" />
            <tattoo_material name="tattoo_male_g_mat" tags="capon" />
            <tattoo_material name="tattoo_male_h_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_i_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_j_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_k_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_l_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_m_mat" tags="capon,turk" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
            <tattoo_material name="scar_male_q" tags="capon" />
            <tattoo_material name="scar_male_o" tags="scar" />
            <tattoo_material name="scar_male_p" tags="scar" />
            <tattoo_material name="scar_male_r" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_female"]/hair_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <hair_mesh name="female_hair_z_h" cover_type1="female_hair_z_h_a" cover_type4="female_hair_z_h">
                <style_tags>
                    <style_tag name="High Ponytail" />
                </style_tags>
            </hair_mesh>
            <hair_mesh name="female_hair_z_i" cover_type1="female_hair_z_i_a" cover_type4="female_hair_z_i">
                <style_tags>
                    <style_tag name="High Ponytail" />
                </style_tags>
            </hair_mesh>
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_female"]/eyebrow_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <eyebrow_mesh name="female_eyebrow_2" />
            <eyebrow_mesh name="female_eyebrow_3" />
            <eyebrow_mesh name="female_eyebrow_4" />
            <xsl:copy-of select="*" />
            <eyebrow_mesh name="female_eyebrow_7" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_female"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="100">
            <tattoo_material name="tattoo_female_a_mat" tags="tattoo1">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_female_b_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_c_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_d_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_e_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_f_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_g_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_h_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_i_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_j_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_k_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_l_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_m_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_n_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_o_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_p_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_q_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_r_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_s_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_t_mat" tags="tattoo1" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <face_texture name="head_female_a" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
            <xsl:copy-of select="*" />
            <face_texture name="head_female_b" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture3" />
            <face_texture name="head_female_c" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture4" />
            <face_texture name="head_female_f" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="kid_3_female"]/voice_types'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <voice_type name="female_01" />
            <voice_type name="female_02" />
            <xsl:copy-of select="*" />
            <voice_type name="female_04" />
            <voice_type name="female_05" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="toddler_male"]/eyebrow_meshes'>
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

    <xsl:template match='skin[@name="toddler_male"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="100">
            <tattoo_material name="tattoo_male_a_mat" tags="capon">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_male_b_mat" tags="capon" />
            <tattoo_material name="tattoo_male_c_mat" tags="capon" />
            <tattoo_material name="tattoo_male_d_mat" tags="capon" />
            <tattoo_material name="tattoo_male_e_mat" tags="capon" />
            <tattoo_material name="tattoo_male_f_mat" tags="capon" />
            <tattoo_material name="tattoo_male_g_mat" tags="capon" />
            <tattoo_material name="tattoo_male_h_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_i_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_j_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_k_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_l_mat" tags="capon,turk" />
            <tattoo_material name="tattoo_male_m_mat" tags="capon,turk" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
            <tattoo_material name="scar_male_q" tags="capon" />
            <tattoo_material name="scar_male_o" tags="scar" />
            <tattoo_material name="scar_male_p" tags="scar" />
            <tattoo_material name="scar_male_r" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="toddler_male"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture1,face_texture2" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture3,face_texture4" />
            <face_texture name="head_male_e" lod_material="head_male_a.lod" color="0xFFFFFFFF" tags="face_texture5,face_texture6,face_texture7" />
            <xsl:copy-of select="*" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="toddler_male"]/voice_types/voice_type[@name="female_01"]'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:copy-of select="*" />
            <voice_type name="male_03" />
            <voice_type name="male_04" />
            <voice_type name="male_05" />
            <voice_type name="male_06" />
            <voice_type name="male_07" />
            <voice_type name="male_08" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="toddler_female"]/eyebrow_meshes'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <eyebrow_mesh name="female_eyebrow_2" />
            <eyebrow_mesh name="female_eyebrow_3" />
            <eyebrow_mesh name="female_eyebrow_4" />
            <xsl:copy-of select="*" />
            <eyebrow_mesh name="female_eyebrow_7" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="toddler_female"]/beard_meshes'>
        <xsl:copy-of select="." />
        <tattoo_materials group_id="8" zero_probability="100">
            <tattoo_material name="tattoo_female_a_mat" tags="tattoo1">
                <style_tags>
                    <style_tag name="eastern" />
                </style_tags>
            </tattoo_material>
            <tattoo_material name="tattoo_female_b_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_c_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_d_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_e_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_f_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_g_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_h_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_i_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_j_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_k_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_l_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_m_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_n_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_o_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_p_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_q_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_r_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_s_mat" tags="tattoo1" />
            <tattoo_material name="tattoo_female_t_mat" tags="tattoo1" />
            <tattoo_material name="scar_male_a" tags="scar" />
            <tattoo_material name="scar_male_b" tags="scar" />
            <tattoo_material name="scar_male_c" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_d" tags="scar" />
            <tattoo_material name="scar_male_e" tags="scar" />
            <!-- <tattoo_material name="scar_male_f" tags="scar" /> -->
            <tattoo_material name="scar_male_g" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_h" tags="scar" />
            <tattoo_material name="scar_male_i" tags="scar,blindrighteye" />
            <tattoo_material name="scar_male_j" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_k" tags="scar" />
            <tattoo_material name="scar_male_l" tags="scar" />
            <tattoo_material name="scar_male_m" tags="scar,blindlefteye" />
            <tattoo_material name="scar_male_n" tags="scar" />
        </tattoo_materials>
    </xsl:template>

    <xsl:template match='skin[@name="toddler_female"]/face_textures'>
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <face_texture name="head_female_a" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
            <face_texture name="head_female_e" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture2" />
            <face_texture name="head_female_b" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture3" />
            <face_texture name="head_female_c" lod_material="head_female_a.lod" color="0xFFCAD3E0" tags="face_texture4" />
            <xsl:copy-of select="*" />
        </xsl:copy>
    </xsl:template>

    <xsl:template match='skin[@name="toddler_female"]/voice_types'>
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