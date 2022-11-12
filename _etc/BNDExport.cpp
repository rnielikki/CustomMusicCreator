#pragma once

#include "BND.hpp"

extern "C" {
    __declspec(dllexport) BND* BND_Create() { return new BND(); }
	__declspec(dllexport) void BND_Delete(BND* bnd) { delete bnd; }
    __declspec(dllexport) bool Load(BND* bnd, const char* file, bool log = true) {
        return bnd->load(file, log);
    }
    __declspec(dllexport) uint32_t Count_Files(BND* bnd) { return bnd->count_files(); }
    __declspec(dllexport) char* Get_Full_Name(BND* bnd, int id) {
        std::string fullName = bnd->get_full_name(id);
        int c_str_len = fullName.length() + 1;
        auto returnVal = (char*)malloc(sizeof(char) * c_str_len);

        strcpy_s(returnVal, c_str_len, fullName.c_str());

        return returnVal;
    }
    __declspec(dllexport) void Replace_File(BND* bnd, int id, const char* path) { bnd->replace_file(id, path); }
    __declspec(dllexport) void Save(BND* bnd, const char* path) { bnd->save(path); }
}
